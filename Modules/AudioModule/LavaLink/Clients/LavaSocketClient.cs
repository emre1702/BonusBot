using BonusBot.AudioModule.Extensions;
using BonusBot.AudioModule.Language;
using BonusBot.AudioModule.LavaLink.Enums;
using BonusBot.AudioModule.LavaLink.Helpers;
using BonusBot.AudioModule.LavaLink.Models;
using BonusBot.AudioModule.LavaLink.Payloads;
using BonusBot.AudioModule.LavaLink.Responses;
using BonusBot.Common.Enums;
using BonusBot.Common.Helper;
using BonusBot.Helper.Events;
using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.LavaLink.Clients
{
    internal class LavaSocketClient
    {
        public static LavaSocketClient Instance => _lazy.Value;

        public Func<LogMessage, Task>? Log { get; set; }

        public AsyncEvent<LavaPlayer>? PlayerConnected { get; set; }
        public AsyncEvent<LavaPlayer>? PlayerDisconnected { get; set; }
        public AsyncEvent<(LavaPlayer Player, AudioTrack? Track, TimeSpan Position)>? PlayerUpdated;
        public AsyncEvent<(int Code, string Reason, bool ByRemote)>? SocketClosed;
        public AsyncEvent<(LavaPlayer Player, LavaLinkTrack? FinishedTrack, string Error)>? TrackException;
        public AsyncEvent<(LavaPlayer Player, LavaLinkTrack? FinishedTrack, TrackEndReason Reason)>? TrackFinished;
        public AsyncEvent<(LavaPlayer Player, LavaLinkTrack FinishedTrack, long TimeoutMs)>? TrackStuck;

        protected Configuration? Configuration { get; set; }

        private readonly ConcurrentDictionary<ulong, LavaPlayer> _players = new();
        private BaseSocketClient? _socketClient;
        private SocketHelper? _socketHelper;
        private readonly TrackHelper _trackHelper = new();
        private static readonly Lazy<LavaSocketClient> _lazy = new(() => new(), true);

        private LavaSocketClient()
        {
            this.AddEvents();
        }

        public Task Start(DiscordSocketClient client, Configuration? configuration = null)
        {
            client.Disconnected += OnDisconnected;
            configuration ??= new Configuration();
            return Initialize(client, configuration);
        }

        private async Task Initialize(BaseSocketClient socketClient, Configuration configuration)
        {
            _socketClient = socketClient;
            var shards = await socketClient.GetRecommendedShardCountAsync();

            Configuration = configuration.WithInternals(socketClient.CurrentUser.Id, shards);
            socketClient.UserVoiceStateUpdated += OnUserVoiceStateUpdated;
            socketClient.VoiceServerUpdated += OnVoiceServerUpdated;

            _socketHelper = new SocketHelper(configuration, Log);
            _socketHelper.OnMessage += OnMessage;
            _socketHelper.OnClosed += OnClosed;

            await _socketHelper.ConnectAsync().ConfigureAwait(false);
        }

        public async Task<LavaPlayer> Connect(IVoiceChannel voiceChannel, ITextChannel? textChannel = null)
        {
            if (_players.TryGetValue(voiceChannel.GuildId, out var player))
                return player;
            if (_socketHelper is null)
                throw new InvalidOperationException(ModuleTexts.NotInitializedYetError);

            await voiceChannel.ConnectAsync(Configuration?.SelfDeaf == true, false, true).ConfigureAwait(false);
            player = new LavaPlayer(voiceChannel, textChannel, _socketHelper);
            _players.TryAdd(voiceChannel.GuildId, player);

            await (PlayerConnected?.InvokeAsync(player) ?? Task.CompletedTask);

            return player;
        }

        private Task OnVoiceServerUpdated(SocketVoiceServer server)
        {
            if (!server.Guild.HasValue || !_players.TryGetValue(server.Guild.Id, out var player))
                return Task.CompletedTask;

            var update = new VoiceServerPayload(server, player.CachedState.VoiceSessionId);
            return (_socketHelper?.SendPayload(update) ?? Task.CompletedTask);
        }

        private Task OnUserVoiceStateUpdated(SocketUser user, SocketVoiceState oldState, SocketVoiceState newState)
        {
            var channel = (oldState.VoiceChannel ?? newState.VoiceChannel);

            SaveStateToPlayer(user, channel.Guild.Id, newState);
            CheckAutoDisconnect(channel);

            return Task.CompletedTask;
        }

        private void SaveStateToPlayer(SocketUser user, ulong guildId, SocketVoiceState state)
        {
            if (_players.TryGetValue(guildId, out var player) && user.Id == _socketClient?.CurrentUser.Id)
                player.CachedState = state;
        }

        private void CheckAutoDisconnect(SocketVoiceChannel voiceChannel)
        {
            if (Configuration?.AutoDisconnect != true)
                return;

            var guildId = voiceChannel.Guild.Id;
            var channelHasUsers = voiceChannel.Users.Any(x => !x.IsBot);
            if (!channelHasUsers)
                StartAutoDisconnect(guildId);
            else
                StopAutoDisconnect(guildId);
        }

        private void StartAutoDisconnect(ulong guildId)
        {
            if (!_players.TryGetValue(guildId, out var player))
                return;
            if (Configuration is null)
                return;
            if (player.Status == PlayerStatus.Disconnected)
                return;
            if (player.DisconnectToken is { })
                return;

            Log?.WriteLog(LogSeverity.Warning, $"Automatically disconnecting in {Configuration.InactivityTimeout.TotalSeconds} seconds.");

            player.DisconnectToken = new();
            Task.Run(async () =>
            {
                await Task.Delay(Configuration.InactivityTimeout).ConfigureAwait(false);
                if (player.DisconnectToken?.IsCancellationRequested == true)
                    return;
                if (player.Status == PlayerStatus.Playing)
                    await player.Stop().ConfigureAwait(false);
                await DisconnectPlayer(player.VoiceChannel).ConfigureAwait(false);
            }, player.DisconnectToken.Token);
        }

        private void StopAutoDisconnect(ulong guildId)
        {
            if (!_players.TryGetValue(guildId, out var player))
                return;

            if (player.DisconnectToken is null)
                return;
            player.DisconnectToken.Cancel(false);
            player.DisconnectToken = null;
        }

        public async Task DisconnectPlayer(IVoiceChannel voiceChannel)
        {
            if (!_players.TryRemove(voiceChannel.GuildId, out var player))
                return;

            player.Dispose();
            await voiceChannel.DisconnectAsync();
            var destroyPayload = new DestroyPayload(voiceChannel.GuildId);
            await _socketHelper!.SendPayload(destroyPayload);

            await (PlayerDisconnected?.InvokeAsync(player) ?? Task.CompletedTask);
        }

        private async Task DisconnectAllPlayers()
        {
            foreach (var player in _players.Values)
                await DisconnectPlayer(player.VoiceChannel);
        }

        public Task MoveChannels(IVoiceChannel voiceChannel)
        {
            if (!_players.TryGetValue(voiceChannel.GuildId, out var player))
                return Task.CompletedTask;
            return player.MoveChannels(voiceChannel, Configuration?.SelfDeaf == true);
        }

        public void SetTextChannel(ulong guildId, ITextChannel textChannel)
        {
            if (!_players.TryGetValue(guildId, out var player))
                return;

            player.TextChannel = textChannel;
        }

        public LavaPlayer? GetPlayer(ulong guildId)
            => _players.TryGetValue(guildId, out var player) ? player : default;

        private async Task OnDisconnected(Exception exception)
        {
            if (Configuration?.PreservePlayers == true)
                return;

            await DisconnectAllPlayers();

            ConsoleHelper.Log(LogSeverity.Debug, LogSource.Module, "Websocket disconnected! Disposing all connected players.", exception);
        }

        private async Task OnClosed(SocketHelper _)
        {
            if (Configuration?.PreservePlayers == true)
                return;

            await DisconnectAllPlayers();

            Log?.WriteLog(LogSeverity.Warning, "Lavalink died. Disposed all players.");
        }

        private async Task OnMessage(string message)
        {
            Log?.WriteLog(LogSeverity.Debug, message);

            using var doc = JsonDocument.Parse(message);
            var json = doc.RootElement;

            var opCode = GetOpCodeFromJsonElement(json);

            switch (opCode)
            {
                case "playerUpdate":
                    await HandleMessagePlayerUpdate(json);
                    break;

                case "stats":
                    /*ServerStats = json.ToObject<ServerStats>();
                    OnServerStats?.Invoke(ServerStats);*/
                    break;

                case "event":
                    await HandleMessageEvent(json);
                    break;

                default:
                    Log?.WriteLog(LogSeverity.Warning, $"Missing handling of {opCode} OP code.");
                    break;
            }
        }

        private ulong GetGuildIdFromJsonElement(JsonElement json)
        {
            if (json.TryGetProperty("guildId", out var guildIdElement))
            {
                var guildIdStr = guildIdElement.ToString();
                if (ulong.TryParse(guildIdStr, out var guildId))
                    return guildId;
            }

            return 0;
        }

        private string GetOpCodeFromJsonElement(JsonElement json)
            => json.GetProperty("op")!.GetString()!;

        private async Task HandleMessagePlayerUpdate(JsonElement json)
        {
            var guildId = GetGuildIdFromJsonElement(json);
            if (!_players.TryGetValue(guildId, out var player))
                return;

            var stateJson = json.GetProperty("state")!.GetRawText();
            var state = JsonSerializer.Deserialize<PlayerState>(stateJson);

            if (player.CurrentTrack is { })
                player.CurrentTrack.Audio.Info.Position = state.Position;
            player.LastUpdate = state.Time;

            await (PlayerUpdated?.InvokeAsync((player, player.CurrentTrack, state.Position)) ?? Task.CompletedTask);
        }

        private async Task HandleMessageEvent(JsonElement json)
        {
            var guildId = GetGuildIdFromJsonElement(json);
            var evt = JsonSerializer.Deserialize<EventType>(json.GetProperty("type")!.GetRawText());
            if (!_players.TryGetValue(guildId, out var player))
                return;
            LavaLinkTrack? track = null;
            if (json.TryGetProperty("track", out var trackProp))
                track = _trackHelper.DecodeTrack(trackProp.GetString() ?? string.Empty);

            switch (evt)
            {
                case EventType.TrackEnd:
                    await HandleMessageEventTrackEnd(json, player, track);
                    break;

                case EventType.TrackException:
                    await HandleMessageEventException(json, player, track);
                    break;

                case EventType.TrackStuck:
                    await HandleMessageEventTrackStuck(json, player, track!);
                    break;

                case EventType.WebSocketClosed:
                    await HandleMessageEventWebSocketClosed(json);
                    break;

                default:
                    Log?.WriteLog(LogSeverity.Warning, $"Missing implementation of {evt} event.");
                    break;
            }
        }

        private async Task HandleMessageEventTrackEnd(JsonElement json, LavaPlayer player, LavaLinkTrack? track)
        {
            var endReason = Enum.Parse<TrackEndReason>(json.GetProperty("reason")!.GetString()!, true);
            if (endReason != TrackEndReason.Replaced)
            {
                await player.SetCurrentTrack(null);
                if (player.Status != PlayerStatus.Stopped)
                    await player.SetStatus(PlayerStatus.Ended);
            }
            await (TrackFinished?.InvokeAsync((player, track, endReason)) ?? Task.CompletedTask);
        }

        private async Task HandleMessageEventException(JsonElement json, LavaPlayer player, LavaLinkTrack? track)
        {
            var error = json.GetProperty("error")!.GetString()!;
            await (TrackException?.InvokeAsync((player, track, error)) ?? Task.CompletedTask);
        }

        private async Task HandleMessageEventTrackStuck(JsonElement json, LavaPlayer player, LavaLinkTrack track)
        {
            var timeoutMs = json.GetProperty("thresholdMs")!.GetInt64()!;
            await (TrackStuck?.InvokeAsync((player, track, timeoutMs)) ?? Task.CompletedTask);
        }

        private Task HandleMessageEventWebSocketClosed(JsonElement json)
        {
            var reason = json.GetProperty("reason")!.GetString()!;
            var code = json.GetProperty("code")!.GetInt32()!;
            var byRemote = json.GetProperty("byRemote")!.GetBoolean()!;
            return (SocketClosed?.InvokeAsync((code, reason, byRemote)) ?? Task.CompletedTask);
        }
    }
}
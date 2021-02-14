using BonusBot.AudioModule.Language;
using BonusBot.AudioModule.LavaLink.Enums;
using BonusBot.AudioModule.LavaLink.Helpers;
using BonusBot.AudioModule.LavaLink.Models;
using BonusBot.AudioModule.LavaLink.Payloads;
using BonusBot.Helper.Events;
using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.LavaLink
{
    internal class LavaPlayer : IDisposable
    {
        public int CurrentVolume { get; private set; } = 100;
        public AudioTrack? CurrentTrack { get; private set; }
        public AudioTrack? PreviousTrack { get; private set; }
        public PlayerStatus Status { get; private set; }
        public LavaQueue<AudioTrack> Queue { get; private set; } = new();
        public SocketVoiceState CachedState { get; set; }

        public IVoiceChannel VoiceChannel { get; set; }
        public ITextChannel? TextChannel { get; set; }
        public List<LavaLinkTrack>? LastSearchResult { get; set; }

        public CancellationTokenSource? DisconnectToken { get; set; }

        public AsyncEvent<(PlayerStatus Old, PlayerStatus New)>? StatusChanged { get; set; }
        public AsyncEvent<AudioTrack?>? TrackChanged { get; set; }
        public AsyncEvent<int>? VolumeChanged { get; set; }
        public DateTimeOffset LastUpdate { get; internal set; }
        public SocketGuild Guild => (SocketGuild)VoiceChannel.Guild;

        public event QueueChangedDelegate QueueChanged
        {
            add => Queue.QueueChanged += value;
            remove => Queue.QueueChanged -= value;
        }

        private readonly SocketHelper _socketHelper;

        public LavaPlayer(IVoiceChannel voiceChannel, ITextChannel? textChannel, SocketHelper socketHelper)
        {
            VoiceChannel = voiceChannel;
            TextChannel = textChannel;
            _socketHelper = socketHelper;
        }

        public async Task Play(AudioTrack? track, bool noReplace = false)
        {
            if (CurrentTrack is { })
                PreviousTrack = CurrentTrack;
            CurrentTrack = track;

            if (track is { })
            {
                var payload = new PlayPayload(VoiceChannel.GuildId, track.Audio.Hash, noReplace);
                await _socketHelper.SendPayload(payload).ConfigureAwait(false);
            }

            await AfterPlay(track).ConfigureAwait(false);
        }

        public async Task Play(AudioTrack? track, TimeSpan startTime, TimeSpan stopTime, bool noReplace = false)
        {
            if (startTime.TotalMilliseconds < 0 || stopTime.TotalMilliseconds < 0)
                throw new InvalidOperationException(ModuleTexts.NegativeStartOrStopError);

            if (startTime <= stopTime)
                throw new InvalidOperationException(ModuleTexts.StartTimeLessThanStopTimeError);

            if (CurrentTrack is { })
                PreviousTrack = CurrentTrack;
            CurrentTrack = track;

            if (track is { })
            {
                var payload = new PlayPayload(VoiceChannel.GuildId, track.Audio.Hash, startTime, stopTime, noReplace);
                await _socketHelper.SendPayload(payload).ConfigureAwait(false);
            }

            await AfterPlay(track).ConfigureAwait(false);
        }

        private async ValueTask AfterPlay(AudioTrack? track)
        {
            if (Status != PlayerStatus.Paused && track is { })
                await SetStatus(PlayerStatus.Playing).ConfigureAwait(false);
            else if (track is null)
                await SetStatus(PlayerStatus.Stopped).ConfigureAwait(false);

            if (TrackChanged is { })
                await TrackChanged.InvokeAsync(track).ConfigureAwait(false);
        }

        public async Task Stop()
        {
            if (CurrentTrack is null)
                throw new InvalidOperationException(ModuleTexts.NothingPlayingError);

            var payload = new StopPayload(VoiceChannel.GuildId);
            await _socketHelper.SendPayload(payload).ConfigureAwait(false);

            CurrentTrack = null;
            Queue.Clear();
            await SetStatus(PlayerStatus.Stopped).ConfigureAwait(false);
        }

        public async Task Resume()
        {
            var payload = new PausePayload(VoiceChannel.GuildId, false);
            await _socketHelper.SendPayload(payload).ConfigureAwait(false);

            if (CurrentTrack is { })
                await SetStatus(PlayerStatus.Playing).ConfigureAwait(false);
            else
                await SetStatus(PlayerStatus.Ended).ConfigureAwait(false);
        }

        public async Task Pause()
        {
            var payload = new PausePayload(VoiceChannel.GuildId, true);
            await _socketHelper.SendPayload(payload).ConfigureAwait(false);

            await SetStatus(PlayerStatus.Paused).ConfigureAwait(false);
        }

        public async Task<AudioTrack?> Skip()
        {
            var previousTrack = CurrentTrack;
            if (!Queue.TryDequeue(out var track))
            {
                await Stop();
                return previousTrack;
            }

            await Play(track);
            return previousTrack;
        }

        public async Task SetVolume(int volume)
        {
            var payload = new VolumePayload(VoiceChannel.GuildId, volume);
            await _socketHelper.SendPayload(payload);

            CurrentVolume = Math.Max(0, Math.Min(1000, volume));
            if (VolumeChanged is { })
                await VolumeChanged.InvokeAsync(CurrentVolume).ConfigureAwait(false);
        }

        public Task Seek(TimeSpan position)
        {
            if (CurrentTrack is null)
                throw new InvalidOperationException(ModuleTexts.NothingPlayingError);

            if (position > CurrentTrack.Audio.Info.Length)
                throw new ArgumentOutOfRangeException(string.Format(ModuleTexts.SeekPositionTooHigh, position.ToString(), CurrentTrack.Audio.Info.Length.ToString()));

            var payload = new SeekPayload(VoiceChannel.GuildId, position);
            return _socketHelper.SendPayload(payload);
        }

        public Task EqualizerAsync(List<EqualizerBand> bands)
        {
            if (CurrentTrack is null)
                throw new InvalidOperationException(ModuleTexts.NothingPlayingError);

            var payload = new EqualizerPayload(VoiceChannel.GuildId, bands);
            return _socketHelper.SendPayload(payload);
        }

        public Task Equalizer(params EqualizerBand[] bands)
        {
            if (CurrentTrack is null)
                throw new InvalidOperationException(ModuleTexts.NothingPlayingError);

            var payload = new EqualizerPayload(VoiceChannel.GuildId, bands);
            return _socketHelper.SendPayload(payload);
        }

        public async ValueTask SetStatus(PlayerStatus newStatus)
        {
            var oldStatus = Status;
            Status = newStatus;
            if (StatusChanged is { })
                await StatusChanged.InvokeAsync((oldStatus, Status)).ConfigureAwait(false);
        }

        public async ValueTask SetCurrentTrack(AudioTrack? track)
        {
            if (CurrentTrack is { })
                PreviousTrack = CurrentTrack;
            CurrentTrack = track;
            if (TrackChanged is { })
                await TrackChanged.InvokeAsync(track).ConfigureAwait(false);
        }

        public async Task MoveChannels(IVoiceChannel voiceChannel, bool selfDeaf = true)
        {
            if (VoiceChannel.Id == voiceChannel.Id)
                return;

            await Pause().ConfigureAwait(false);
            await VoiceChannel.DisconnectAsync().ConfigureAwait(false);
            await voiceChannel.ConnectAsync(selfDeaf, false, true).ConfigureAwait(false);
            await Resume().ConfigureAwait(false);

            VoiceChannel = voiceChannel;
        }

        public void Dispose()
        {
            Status = PlayerStatus.Disconnected;
            Queue.Clear();
            CurrentTrack = null;
            DisconnectToken?.Cancel(false);
            DisconnectToken = null;
            StatusChanged?.Clear();
            TrackChanged?.Clear();
            VolumeChanged?.Clear();

            GC.SuppressFinalize(this);
        }
    }
}
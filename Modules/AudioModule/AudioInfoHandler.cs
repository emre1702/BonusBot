using BonusBot.AudioModule.Helpers;
using BonusBot.AudioModule.LavaLink;
using BonusBot.AudioModule.LavaLink.Enums;
using BonusBot.AudioModule.LavaLink.Models;
using BonusBot.AudioModule.Models;
using BonusBot.Common.Helper;
using BonusBot.Common.Interfaces.Guilds;
using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace BonusBot.AudioModule
{
    internal class AudioInfoHandler
    {
        private readonly ConcurrentDictionary<ulong, SemaphoreSlim> _guildLocks = new ConcurrentDictionary<ulong, SemaphoreSlim>();

        private readonly IGuildsHandler _guildsHandler;

        public AudioInfoHandler(IGuildsHandler guildsHandler) => _guildsHandler = guildsHandler;

        public Task TrackChanged(LavaPlayer player, AudioTrack? audioTrack)
            => Do(player, embedInfo => AudioInfoEmbedHelper.UpdateEmbed(embedInfo, player, embed => GetWithNewAudioTrackInfo(embed, audioTrack))).AsTask();

        public Task VolumeChanged(LavaPlayer player, int volume)
            => Do(player, embedInfo => embedInfo.UpdateEmbed(player, embed => GetWithNewVolume(embed, volume))).AsTask();

        public Task QueueChanged(LavaPlayer player, LavaQueue<AudioTrack> queue)
            => Do(player, embedInfo => embedInfo.UpdateEmbed(player, embed => GetWithNewQueueData(embed, queue))).AsTask();

        public Task StatusChanged(LavaPlayer player, PlayerStatus newStatus)
           => Do(player, embedInfo => embedInfo.UpdateEmbed(player, embed => GetWithNewStatus(embed, newStatus))).AsTask();

        private async ValueTask Do(LavaPlayer player, Func<EmbedInfo, Task> func)
        {
            var semaphore = _guildLocks.GetOrAdd(player.VoiceChannel.Guild.Id, new SemaphoreSlim(1, 1));
            await semaphore.WaitAsync();
            try
            {
                var channel = await GetAudioInfoChannel(player.Guild);
                if (channel is null) return;
                var embedInfo = await AudioInfoEmbedHelper.GetEmbedInfo(channel);

                await func(embedInfo);
            }
            catch (Exception ex)
            {
                ConsoleHelper.Log(LogSeverity.Error, Common.Enums.LogSource.AudioModule, "Exception in AudioInfoHandler do occured.", ex);
            }
            finally
            {
                semaphore.Release();
            }
        }

        private ValueTask Do(LavaPlayer player, Action<EmbedInfo> func)
        {
            return Do(player, embedInfo =>
            {
                func(embedInfo);
                return Task.CompletedTask;
            });
        }

        private async Task<SocketTextChannel?> GetAudioInfoChannel(SocketGuild guild)
        {
            var bonusGuild = _guildsHandler.GetGuild(guild);
            if (bonusGuild is null) return null;

            return await bonusGuild.Settings.Get<SocketTextChannel>(GetType().Assembly, Settings.AudioInfoChannelId);
        }

        private async Task<Embed> GetWithNewAudioTrackInfo(Embed embed, AudioTrack? audioTrack)
        {
            var builder = embed.ToEmbedBuilder();
            await builder.AddAudioTrackInfo(audioTrack);
            builder.WithCurrentTimestamp();

            return builder.Build();
        }

        private Embed GetWithNewVolume(Embed embed, int volume)
        {
            var builder = embed.ToEmbedBuilder();
            builder.AddVolumeInfo(volume);
            builder.WithCurrentTimestamp();

            return builder.Build();
        }

        private Embed GetWithNewQueueData(Embed embed, LavaQueue<AudioTrack> queue)
        {
            var builder = embed.ToEmbedBuilder();
            builder.AddQueueInfo(queue);
            builder.WithCurrentTimestamp();

            return builder.Build();
        }

        private Embed GetWithNewStatus(Embed embed, PlayerStatus status)
        {
            var builder = embed.ToEmbedBuilder();
            builder.AddStatusInfo(status);
            builder.WithCurrentTimestamp();

            return builder.Build();
        }
    }
}
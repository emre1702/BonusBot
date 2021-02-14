using BonusBot.AudioModule.Helpers;
using BonusBot.AudioModule.LavaLink;
using BonusBot.AudioModule.LavaLink.Enums;
using BonusBot.AudioModule.LavaLink.Models;
using BonusBot.AudioModule.Models;
using BonusBot.Common.Extensions;
using BonusBot.Common.Helper;
using BonusBot.Database;
using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BonusBot.AudioModule
{
    internal class AudioInfoHandler
    {
        private readonly ConcurrentDictionary<ulong, SemaphoreSlim> _guildLocks = new ConcurrentDictionary<ulong, SemaphoreSlim>();

        private readonly BonusDbContextFactory _bonusDbContextFactory;

        public AudioInfoHandler(BonusDbContextFactory bonusDbContextFactory) => _bonusDbContextFactory = bonusDbContextFactory;

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
            try
            {
                var channel = await GetAudioInfoChannel(player.Guild);
                if (channel is null) return;
                var embedInfo = await AudioInfoEmbedHelper.GetEmbedInfo(channel);

                var semaphore = _guildLocks.GetOrAdd(player.VoiceChannel.Guild.Id, new SemaphoreSlim(1, 1));
                await semaphore.WaitAsync();
                try
                {
                    await func(embedInfo);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    semaphore.Release();
                }
            }
            catch (Exception ex)
            {
                ConsoleHelper.Log(LogSeverity.Error, Common.Enums.LogSource.AudioModule, "Exception in AudioInfoHandler do occured.", ex);
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
            using var dbContext = _bonusDbContextFactory.CreateDbContext();
            var channelId = await dbContext.GuildsSettings.GetUInt64(guild.Id, Settings.AudioInfoChannelId, GetType().Assembly);
            if (!channelId.HasValue)
                return null;
            return guild.GetTextChannel(channelId.Value);
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
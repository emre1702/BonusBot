using BonusBot.AudioModule.Extensions;
using BonusBot.AudioModule.LavaLink;
using BonusBot.AudioModule.LavaLink.Enums;
using BonusBot.AudioModule.LavaLink.Models;
using BonusBot.AudioModule.Models;
using BonusBot.Common.Helper;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.Helpers
{
    internal static class AudioInfoEmbedHelper
    {
        internal static async Task<EmbedInfo> GetEmbedInfo(ISocketMessageChannel textChannel)
        {
            var messagesList = await textChannel.GetMessagesAsync(1).FlattenAsync();
            var message = messagesList.FirstOrDefault();
            var embed = message?.Embeds.FirstOrDefault();

            var info = new EmbedInfo(textChannel, embed as Embed, message);
            return info;
        }

        internal static async Task UpdateEmbed(this EmbedInfo embedInfo, LavaPlayer player, Func<Embed, Task<Embed>> embedRefresher)
        {
            if (embedInfo is null)
                return;
            if (embedInfo.Embed is null)
            {
                embedInfo.Embed = await GetNewAudioInfo(player);
                var msg = await embedInfo.Channel.SendMessageAsync(embed: embedInfo.Embed);
            }
            else
            {
                embedInfo.Embed = await embedRefresher(embedInfo.Embed);
                await (((RestUserMessage?)embedInfo.Message)?.ModifyAsync(msg => msg.Embed = embedInfo.Embed) ?? Task.CompletedTask);
            }
        }

        internal static Task UpdateEmbed(this EmbedInfo embedInfo, LavaPlayer player, Func<Embed, Embed> embedRefresher)
        {
            return UpdateEmbed(embedInfo, player, (embed) =>
            {
                embed = embedRefresher(embed);
                return Task.FromResult(embed);
            });
        }

        internal static async Task AddAudioTrackInfo(this EmbedBuilder builder, AudioTrack? audioTrack)
        {
            if (audioTrack is { })
            {
                builder.Author = new EmbedAuthorBuilder() { Name = audioTrack.RequestedBy.Username, IconUrl = audioTrack.RequestedBy.GetAvatarUrl() };
                builder.Title = audioTrack.Audio.Info.Title;
                builder.Url = audioTrack.Audio.Info.Uri.AbsoluteUri;
                builder.ThumbnailUrl = await ThumbnailHelper.Instance.FetchThumbnail(audioTrack.Audio);
                builder.Fields[2].Value = audioTrack.Audio.Info.Length.ToString();
                builder.Fields[3].Value = audioTrack.AddedTime.ToString();
            }
            else
                SetEmptyAudioTrackInfo(builder);
        }

        internal static void AddQueueInfo(this EmbedBuilder builder, LavaQueue<AudioTrack> queue)
        {
            var queueStr = queue?.ToString();
            builder.Fields[4].Value = string.IsNullOrEmpty(queueStr) ? "-" : queueStr;
        }

        internal static void AddVolumeInfo(this EmbedBuilder builder, int volume)
        {
            builder.Fields[1].Value = volume;
        }

        internal static void AddStatusInfo(this EmbedBuilder builder, PlayerStatus playerStatus)
        {
            builder.Fields[0].Value = playerStatus.ToString();
        }

        private static void SetEmptyAudioTrackInfo(EmbedBuilder builder)
        {
            builder.Author = null;
            builder.Title = null;
            builder.Url = null;
            builder.ThumbnailUrl = null;
            builder.Fields[2].Value = "-";
            builder.Fields[3].Value = "-";
        }

        private static async Task<Embed> GetNewAudioInfo(LavaPlayer player)
        {
            var builder = DefaultAudioInfo;
            await AddAudioTrackInfo(builder, player.CurrentTrack);
            AddQueueInfo(builder, player.Queue);
            AddVolumeInfo(builder, player.CurrentVolume);

            return builder.Build();
        }

        public static EmbedBuilder DefaultAudioInfo
            => EmbedHelper.DefaultEmbed
            .WithColor(0, 0, 150)
            .WithFooter("Audio info")
            .WithFields(
                new EmbedFieldBuilder().WithIsInline(true).WithName("Status:").WithValue("playing"),
                new EmbedFieldBuilder().WithIsInline(true).WithName("Volume:"),
                new EmbedFieldBuilder().WithIsInline(true).WithName("Length:"),
                new EmbedFieldBuilder().WithIsInline(true).WithName("Added:"),
                new EmbedFieldBuilder().WithIsInline(false).WithName("Queue:")
            );
    }
}
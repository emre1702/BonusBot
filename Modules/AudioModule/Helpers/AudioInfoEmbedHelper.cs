using BonusBot.AudioModule.Extensions;
using BonusBot.AudioModule.Language;
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
using System.Text;
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
            builder.Fields.RemoveRange(4, builder.Fields.Count - 4);
            if (queue.Count <= 10)
                AddQueueShortInfo(builder, queue);
            else
                AddQueueLongInfo(builder, queue);
        }

        private static void AddQueueShortInfo(EmbedBuilder builder, LavaQueue<AudioTrack> queue)
        {
            var queueStr = queue.ToString();
            if (queueStr.Length > EmbedFieldBuilder.MaxFieldValueLength)
            {
                AddQueueLongInfo(builder, queue);
                return;
            }
            builder.WithAddedQueueInfoField(queueStr);
        }

        private static void AddQueueLongInfo(EmbedBuilder builder, LavaQueue<AudioTrack> queue)
        {
            StringBuilder strBuilder = new();
            var tracks = queue!.Items;
            for (int trackIndex = 0; trackIndex < tracks.Count; ++trackIndex)
            {
                var str = (trackIndex + 1) + tracks[trackIndex].ToString();
                if (strBuilder.Length + str.Length + 1 > EmbedFieldBuilder.MaxFieldValueLength)
                {
                    builder = builder.WithAddedQueueInfoField(strBuilder.ToString());
                    strBuilder.Clear();
                }
                strBuilder.AppendLine(str);
            }
            if (strBuilder.Length > 0)
                builder.WithAddedQueueInfoField(strBuilder.ToString());
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
            .WithFooter(ModuleTexts.AudioInfoFooter)
            .WithFields(
                new EmbedFieldBuilder().WithIsInline(true).WithName(ModuleTexts.Status + ":").WithValue("playing"),
                new EmbedFieldBuilder().WithIsInline(true).WithName(ModuleTexts.Volume + ":"),
                new EmbedFieldBuilder().WithIsInline(true).WithName(ModuleTexts.Length + ":"),
                new EmbedFieldBuilder().WithIsInline(true).WithName(ModuleTexts.RequestedBy + ":")
            )
            .WithAddedQueueInfoField("-");

        private static EmbedBuilder WithAddedQueueInfoField(this EmbedBuilder embedBuilder, string queueStr)
            => embedBuilder.AddField(ModuleTexts.Queue + ":", string.IsNullOrWhiteSpace(queueStr) ? "-" : queueStr);
    }
}
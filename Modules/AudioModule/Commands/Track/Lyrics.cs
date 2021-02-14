using BonusBot.AudioModule.Extensions;
using BonusBot.AudioModule.Language;
using BonusBot.AudioModule.LavaLink.Helpers;
using BonusBot.AudioModule.LavaLink.Models;
using BonusBot.AudioModule.Models.CommandArgs;
using BonusBot.Common.Commands;
using BonusBot.Common.Helper;
using Discord;
using System;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.Commands.Track
{
    internal class Lyrics : CommandHandlerBase<Main, EmptyCommandArgs>
    {
        public Lyrics(Main main) : base(main)
        {
        }

        public override async Task Do(EmptyCommandArgs _)
        {
            var audio = Class.Player!.CurrentTrack!.Audio;
            var lyrics = await LyricsHelper.Instance.Search(audio.Info.Title);

            if (string.IsNullOrWhiteSpace(lyrics))
            {
                await Class.ReplyAsync(string.Format(ModuleTexts.LyricsNotFoundError, audio.Info.Title));
                return;
            }

            if (lyrics.Length <= EmbedBuilder.MaxDescriptionLength)
                await OutputEmbedInfo(lyrics, audio);
            else
                await OutputTextInfo(lyrics, audio);
        }

        private async Task OutputEmbedInfo(string lyrics, LavaLinkTrack audio)
        {
            var thumbnail = await ThumbnailHelper.Instance.FetchThumbnail(audio);
            var embed = EmbedHelper.DefaultEmbed
                .WithImageUrl(thumbnail)
                .WithDescription(lyrics)
                .WithAuthor(string.Format(ModuleTexts.LyricsForInfo, audio.Info.Title), thumbnail);
            await Class.ReplyAsync(embed);
        }

        private Task OutputTextInfo(string lyrics, LavaLinkTrack audio)
        {
            var msg = string.Format(ModuleTexts.LyricsForInfo, audio.Info.Title) + Environment.NewLine + lyrics;
            return Class.ReplyAsync(msg);
        }
    }
}
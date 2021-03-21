using BonusBot.AudioModule.Extensions;
using BonusBot.AudioModule.Models.CommandArgs;
using BonusBot.AudioModule.PartialMain;
using BonusBot.Common.Commands;
using BonusBot.Common.Helper;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.Commands.Track
{
    internal class NowPlaying : CommandHandlerBase<Main, EmptyCommandArgs>
    {
        public NowPlaying(Main main) : base(main)
        {
        }

        public override async Task Do(EmptyCommandArgs _)
        {
            var track = Class.Player!.CurrentTrack!.Audio;

            var thumb = await ThumbnailHelper.Instance.FetchThumbnail(track);
            var embed = EmbedHelper.DefaultEmbed
                .WithAuthor($"Now Playing {track.Info.Title}", thumb, $"{track.Info.Uri}")
                .WithThumbnailUrl(thumb)
                .AddField("Author", track.Info.Author, true)
                .AddField("Length", track.Info.Length, true)
                .AddField("Position", track.Info.Position, true)
                .AddField("Streaming?", track.Info.IsStream, true);

            await Class.ReplyAsync(embed);
        }
    }
}
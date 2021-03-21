using BonusBot.AudioModule.Language;
using BonusBot.AudioModule.Models.CommandArgs;
using BonusBot.AudioModule.PartialMain;
using BonusBot.Common.Commands;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.Commands.Track
{
    internal class Skip : CommandHandlerBase<Main, EmptyCommandArgs>
    {
        public Skip(Main main) : base(main)
        {
        }

        public override async Task Do(EmptyCommandArgs _)
        {
            var skipped = await Class.Player!.Skip();
            if (Class.Player.CurrentTrack is { })
                await Class.ReplyAsync(string.Format(ModuleTexts.SkippedTrackNowPlayingInfo, skipped, Class.Player.CurrentTrack));
            else
                await Class.ReplyAsync(string.Format(ModuleTexts.SkippedTrackNowStoppedInfo, skipped));
        }
    }
}
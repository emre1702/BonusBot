using BonusBot.AudioModule.Language;
using BonusBot.AudioModule.LavaLink.Models;
using BonusBot.AudioModule.Models.CommandArgs;
using BonusBot.AudioModule.PartialMain;
using BonusBot.Common.Commands;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.Commands.Track
{
    internal class ReplayPrevious : CommandHandlerBase<Main, EmptyCommandArgs>
    {
        public ReplayPrevious(Main main) : base(main)
        {
        }

        public override async Task Do(EmptyCommandArgs _)
        {
            var trackToReplay = GetTrackToReplay();
            if (trackToReplay is null)
            {
                await Class.ReplyErrorAsync(ModuleTexts.NoTrackToReplayError);
                return;
            }

            await Class.Player!.Play(trackToReplay);
            await Class.ReplyAsync(string.Format(ModuleTexts.ReplayingSongInfo, trackToReplay));
        }

        private AudioTrack? GetTrackToReplay()
            => Class.Player switch
            {
                { PreviousTrack: { } } => Class.Player.PreviousTrack,
                { CurrentTrack: { } } => Class.Player.CurrentTrack,
                _ => null
            };
    }
}
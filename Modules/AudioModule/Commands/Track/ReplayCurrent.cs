using BonusBot.AudioModule.Language;
using BonusBot.AudioModule.LavaLink.Models;
using BonusBot.AudioModule.Models.CommandArgs;
using BonusBot.AudioModule.PartialMain;
using BonusBot.Common.Commands;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.Commands.Track
{
    internal class ReplayCurrent : CommandHandlerBase<Main, EmptyCommandArgs>
    {
        public ReplayCurrent(Main main) : base(main)
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
                { CurrentTrack: { } } => Class.Player.CurrentTrack,
                { PreviousTrack: { } } => Class.Player.PreviousTrack,
                _ => null
            };
    }
}
using BonusBot.AudioModule.Language;
using BonusBot.AudioModule.Models.CommandArgs;
using BonusBot.AudioModule.PartialMain;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.Commands.Track
{
    internal class PlaySoundcloud : SearchBase<Main, PlayArgs>
    {
        public PlaySoundcloud(Main main) : base(main)
        {
        }

        public override async Task Do(PlayArgs args)
        {
            var searchResult = await Main.LavaRestClient.SearchSoundcloud(args.Query);
            if (await CheckHasErrors(searchResult))
                return;
            var audioTrack = GetAudioTrack(searchResult);

            await Class.Player!.Play(audioTrack);
            await Class.ReplyAsync(string.Format(ModuleTexts.NowPlayingInfo, audioTrack));
        }
    }
}
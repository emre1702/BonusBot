using BonusBot.AudioModule.Language;
using BonusBot.AudioModule.Models.CommandArgs;
using BonusBot.AudioModule.PartialMain;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.Commands.Track
{
    internal class QueueSoundcloud : SearchBase<Main, PlayArgs>
    {
        public QueueSoundcloud(Main main) : base(main)
        {
        }

        public override async Task Do(PlayArgs args)
        {
            var searchResult = await Main.LavaRestClient.SearchSoundcloud(args.Query);
            if (await CheckHasErrors(searchResult))
                return;
            var audioTrack = GetAudioTrack(searchResult);

            if (Class.Player!.Queue.Count > 0 || Class.Player.CurrentTrack is { })
            {
                Class.Player.Queue.Enqueue(audioTrack);
                await Class.ReplyAsync(string.Format(ModuleTexts.TrackHasBeenEnqueuedInfo, audioTrack));
            }
            else
                await Class.Player.Play(audioTrack);

        }
    }
}
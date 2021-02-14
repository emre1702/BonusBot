using BonusBot.AudioModule.Language;
using BonusBot.AudioModule.LavaLink.Models;
using BonusBot.AudioModule.Models.CommandArgs;
using System.Linq;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.Commands.Track
{
    internal class QueueYouTube : SearchBase<Main, PlayArgs>
    {
        public QueueYouTube(Main main) : base(main)
        {
        }

        public override async Task Do(PlayArgs args)
        {
            /*  int playlistInUrlIndex = query.IndexOf("&list=");
            if (playlistInUrlIndex >= 0)
            {
                query = query.Substring(0, playlistInUrlIndex);
            }*/

            var searchResult = await Main.LavaRestClient.SearchYouTube(args.Query, 1);
            if (await CheckHasErrors(searchResult))
                return;
            var audioTrack = GetAudioTrack(searchResult);

            if (Class.Player!.Queue.Count > 0 || Class.Player.CurrentTrack is { })
                Class.Player.Queue.Enqueue(audioTrack);
            else
                await Class.Player.Play(audioTrack);
            await Class.ReplyAsync(string.Format(ModuleTexts.TrackHasBeenEnqueuedInfo, audioTrack));
        }

        private AudioTrack GetAudioTrack(SearchResult searchResult)
            => new(searchResult.Tracks.First(), Class.Context.User!);
    }
}
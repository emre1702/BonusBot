using BonusBot.AudioModule.Language;
using BonusBot.AudioModule.LavaLink.Models;
using BonusBot.AudioModule.Models.CommandArgs;
using BonusBot.AudioModule.PartialMain;
using System.Linq;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.Commands.Playlist
{
    internal abstract class PlaylistBase : SearchBase<Main, PlaylistArgs>
    {
        protected PlaylistBase(Main main) : base(main)
        {
        }

        protected async Task HandleSearchResult(SearchResult searchResult, int limit)
        {
            if (await CheckHasErrors(searchResult))
                return;
            var audioTracks = searchResult.Tracks.Take(limit).Select(GetAudioTrack).ToList();

            var firstTrack = audioTracks.First();
            await Class.Player!.Play(firstTrack);
            Class.Player.Queue.Clear();
            Class.Player.Queue.EnqueueRange(audioTracks.Skip(1));

            await Class.ReplyAsync(string.Format(ModuleTexts.PlaylistHasBeenEnqueued, audioTracks.Count));
            await Class.ReplyAsync(string.Format(ModuleTexts.NowPlayingInfo, firstTrack));
        }
    }
}

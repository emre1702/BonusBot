using BonusBot.AudioModule.Language;
using BonusBot.AudioModule.LavaLink.Models;
using BonusBot.AudioModule.Models.CommandArgs;
using BonusBot.AudioModule.PartialMain;
using System.Linq;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.Commands.Playlist
{
    internal abstract class QueuePlaylistBase : SearchBase<Main, PlaylistArgs>
    {
        protected QueuePlaylistBase(Main main) : base(main)
        {
        }

        protected async Task HandleSearchResult(SearchResult searchResult, int limit)
        {
            if (await CheckHasErrors(searchResult))
                return;
            var audioTracks = searchResult.Tracks.Take(limit).Select(GetAudioTrack).ToList();

            if (Class.Player!.CurrentTrack is null)
            {
                var firstTrack = audioTracks.First();
                await Class.Player.Play(firstTrack);
                Class.Player.Queue.EnqueueRange(audioTracks.Skip(1));
                await Class.ReplyAsync(string.Format(ModuleTexts.NowPlayingInfo, firstTrack));
            }
            else
            {
                Class.Player.Queue.EnqueueRange(audioTracks);
                await Class.ReplyAsync(string.Format(ModuleTexts.PlaylistHasBeenEnqueued, audioTracks.Count));
            }
        }
    }
}

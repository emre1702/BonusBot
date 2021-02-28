using BonusBot.AudioModule.Language;
using BonusBot.AudioModule.LavaLink.Models;
using BonusBot.AudioModule.Models.CommandArgs;
using System.Linq;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.Commands.Playlist
{
    internal class PlaylistYouTube : SearchBase<Main, PlaylistArgs>
    {
        public PlaylistYouTube(Main main) : base(main)
        {
        }

        public override async Task Do(PlaylistArgs args)
        {
            var searchResult = await Main.LavaRestClient.SearchYouTube(args.Query);
            if (await CheckHasErrors(searchResult))
                return;
            var audioTracks = searchResult.Tracks.Take(args.Limit).Select(GetAudioTrack).ToList();

            var firstTrack = audioTracks.First();
            await Class.Player!.Play(firstTrack);
            Class.Player.Queue.Clear();
            Class.Player.Queue.EnqueueRange(audioTracks.Skip(1));

            await Class.ReplyAsync(string.Format(ModuleTexts.PlaylistHasBeenEnqueued, audioTracks.Count));
            await Class.ReplyAsync(string.Format(ModuleTexts.NowPlayingInfo, firstTrack));
        }
    }
}
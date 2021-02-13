using BonusBot.AudioModule.Language;
using BonusBot.AudioModule.LavaLink.Enums;
using BonusBot.AudioModule.LavaLink.Models;
using BonusBot.AudioModule.Models.CommandArgs;
using BonusBot.Common.Commands;
using System.Linq;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.Commands.Track
{
    internal class PlayYouTube : CommandHandlerBase<Main, PlayArgs>
    {
        public PlayYouTube(Main main) : base(main)
        {
        }

        public override async Task Do(PlayArgs args)
        {
            /*  int playlistInUrlIndex = query.IndexOf("&list=");
            if (playlistInUrlIndex >= 0)
            {
                query = query.Substring(0, playlistInUrlIndex);
            }*/

            var searchResult = await Main.LavaRestClient.SearchYouTube(args.Query);
            if (await CheckHasErrors(searchResult))
                return;
            var audioTrack = GetAudioTrack(searchResult);

            await Main.Player!.Play(audioTrack);
            await Main.ReplyAsync(string.Format(ModuleTexts.NowPlayingInfo, audioTrack));
        }

        private async Task<bool> CheckHasErrors(SearchResult searchResult)
        {
            if (searchResult.LoadType == LoadType.LoadFailed)
            {
                await Main.ReplyAsync(ModuleTexts.LavaLinkLoadError);
                return true;
            }

            if (searchResult.LoadType == LoadType.NoMatches)
            {
                await Main.ReplyAsync(ModuleTexts.LavaLinkNoMatchesError);
                return true;
            }

            return false;
        }

        private AudioTrack GetAudioTrack(SearchResult searchResult)
            => new(searchResult.Tracks.First(), Main.Context.User!);
    }
}
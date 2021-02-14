using BonusBot.AudioModule.Language;
using BonusBot.AudioModule.LavaLink.Enums;
using BonusBot.AudioModule.LavaLink.Models;
using BonusBot.AudioModule.Models.CommandArgs;
using BonusBot.Common.Commands;
using System.Linq;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.Commands.Track
{
    internal class PlayYouTube : SearchBase<Main, PlayArgs>
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

            var searchResult = await Main.LavaRestClient.SearchYouTube(args.Query, 1);
            if (await CheckHasErrors(searchResult))
                return;
            var audioTrack = GetAudioTrack(searchResult);

            await Class.Player!.Play(audioTrack);
            await Class.ReplyAsync(string.Format(ModuleTexts.NowPlayingInfo, audioTrack));
        }

        private AudioTrack GetAudioTrack(SearchResult searchResult)
            => new(searchResult.Tracks.First(), Class.Context.User!);
    }
}
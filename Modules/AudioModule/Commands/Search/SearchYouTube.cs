using BonusBot.AudioModule.Models.CommandArgs;
using System.Threading.Tasks;
using static BonusBot.AudioModule.Main;

namespace BonusBot.AudioModule.Commands.Search
{
    internal class SearchYouTube : SearchLimitBase<SearchGroup>
    {
        public SearchYouTube(SearchGroup main) : base(main)
        {
        }

        public override async Task Do(SearchArgs args)
        {
            var searchResult = await LavaRestClient.SearchYouTube(args.Query);
            await HandleSearchResult(searchResult);
        }
    }
}
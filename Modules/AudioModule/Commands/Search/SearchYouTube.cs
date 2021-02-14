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

        public override async Task Do(SearchLimitArgs args)
        {
            var searchResult = await Main.LavaRestClient.SearchYouTube(args.Query, args.Limit);
            await HandleSearchResult(searchResult);
        }
    }
}
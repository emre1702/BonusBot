using BonusBot.AudioModule.Models.CommandArgs;
using System.Threading.Tasks;
using static BonusBot.AudioModule.Main;

namespace BonusBot.AudioModule.Commands.Search
{
    internal class SearchSoundcloud : SearchLimitBase<SearchGroup>
    {
        public SearchSoundcloud(SearchGroup main) : base(main)
        {
        }

        public override async Task Do(SearchLimitArgs args)
        {
            var searchResult = await LavaRestClient.SearchSoundcloud(args.Query, args.Limit);
            await HandleSearchResult(searchResult);
        }
    }
}
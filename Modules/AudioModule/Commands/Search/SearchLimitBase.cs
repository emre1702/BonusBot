using BonusBot.AudioModule.Language;
using BonusBot.AudioModule.LavaLink.Models;
using BonusBot.AudioModule.Models;
using BonusBot.AudioModule.Models.CommandArgs;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.Commands.Search
{
    internal abstract class SearchLimitBase<TClass> : SearchBase<TClass, SearchArgs>
        where TClass : AudioCommandBase
    {
        protected SearchLimitBase(TClass main) : base(main)
        {
        }

        protected async Task HandleSearchResult(SearchResult search)
        {
            var tracks = search.Tracks.ToList();
            Class.Player!.LastSearchResult = tracks;

            await Class.ReplyAsync(string.Format(ModuleTexts.FoundTracksAmountInfo, tracks.Count));

            var builder = new StringBuilder();
            for (int i = 0; i < tracks.Count; ++i)
                builder.AppendLine($"{(i + 1)}. {tracks[i]}");

            await Class.ReplyAsync(builder.ToString());
        }
    }
}
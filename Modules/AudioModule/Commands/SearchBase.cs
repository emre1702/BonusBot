using BonusBot.AudioModule.Language;
using BonusBot.AudioModule.LavaLink.Enums;
using BonusBot.AudioModule.LavaLink.Models;
using BonusBot.AudioModule.Models;
using BonusBot.AudioModule.Models.CommandArgs;
using BonusBot.Common.Commands;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.Commands
{
    internal abstract class SearchBase<ClassT, ArgsT> : CommandHandlerBase<ClassT, ArgsT>
        where ClassT : AudioCommandBase
        where ArgsT : IQueryArgs
    {
        protected SearchBase(ClassT main) : base(main)
        {
        }

        protected async Task<bool> CheckHasErrors(SearchResult searchResult)
        {
            if (searchResult.LoadType == LoadType.LoadFailed)
            {
                await Class.ReplyAsync(ModuleTexts.LavaLinkLoadError);
                return true;
            }

            if (searchResult.LoadType == LoadType.NoMatches)
            {
                await Class.ReplyAsync(ModuleTexts.LavaLinkNoMatchesError);
                return true;
            }

            return false;
        }
    }
}
﻿using BonusBot.AudioModule.Language;
using BonusBot.AudioModule.LavaLink.Enums;
using BonusBot.AudioModule.LavaLink.Models;
using BonusBot.AudioModule.Models;
using BonusBot.AudioModule.Models.CommandArgs;
using BonusBot.Common.Commands;
using System.Linq;
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
                await Class.ReplyErrorAsync(ModuleTexts.LavaLinkLoadError);
                return true;
            }

            if (searchResult.LoadType == LoadType.NoMatches)
            {
                await Class.ReplyErrorAsync(ModuleTexts.LavaLinkNoMatchesError);
                return true;
            }

            return false;
        }

        protected AudioTrack GetAudioTrack(LavaLinkTrack track)
            => new(track, Class.Context.GuildUser!);

        protected AudioTrack GetAudioTrack(SearchResult searchResult)
            => GetAudioTrack(searchResult.Tracks.First());
    }
}
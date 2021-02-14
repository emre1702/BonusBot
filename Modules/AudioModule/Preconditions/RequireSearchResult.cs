using BonusBot.AudioModule.Language;
using BonusBot.AudioModule.LavaLink.Clients;
using Discord.Commands;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.Preconditions
{
    internal class RequireSearchResult : PreconditionAttribute
    {
        public override Task<PreconditionResult> CheckPermissionsAsync(ICommandContext context, CommandInfo command, IServiceProvider services)
        {
            var lavaClient = LavaSocketClient.Instance;
            var player = lavaClient.GetPlayer(context.Guild.Id);

            if (player?.LastSearchResult is null)
                return Task.FromResult(PreconditionResult.FromError(ModuleTexts.NoLastSearchError));

            if (player.LastSearchResult.Any() != true)
                return Task.FromResult(PreconditionResult.FromError(ModuleTexts.LastSearchEmptyError));

            return Task.FromResult(PreconditionResult.FromSuccess());
        }
    }
}
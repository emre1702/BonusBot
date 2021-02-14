using BonusBot.AudioModule.Language;
using BonusBot.AudioModule.LavaLink.Clients;
using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.Preconditions
{
    internal class RequireCurrentTrackAttribute : PreconditionAttribute
    {
        public override Task<PreconditionResult> CheckPermissionsAsync(ICommandContext context, CommandInfo command, IServiceProvider services)
        {
            var lavaClient = LavaSocketClient.Instance;
            var player = lavaClient.GetPlayer(context.Guild.Id);

            if (player?.CurrentTrack is null)
                return Task.FromResult(PreconditionResult.FromError(ModuleTexts.NoTrackRunningError));

            return Task.FromResult(PreconditionResult.FromSuccess());
        }
    }
}
using BonusBot.Common.Interfaces.Services;
using BonusBot.Common.Workers;
using BonusBot.Database.Entities.Cases;
using Discord.WebSocket;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BonusBot.AdminModule.Jobs
{
    public class UnbanJob : JobBase
    {
        private readonly ITimedActionsHandler _timedActionsHandler;
        private readonly IDiscordClientHandler _discordClientHandler;

        public UnbanJob(ITimedActionsHandler timedActionsHandler, IDiscordClientHandler discordClientHandler)
            => (_timedActionsHandler, _discordClientHandler) = (timedActionsHandler, discordClientHandler);

        protected override TimeSpan DelayTime => TimeSpan.FromMinutes(1);

        protected override async ValueTask DoWork()
        {
            var client = await _discordClientHandler.ClientSource.Task;

            var unbanActions = _timedActionsHandler.Get(ActionType.Unban, GetType());
            if (!unbanActions.Any()) return;

            foreach (var unbanAction in unbanActions)
            {
                await HandleAction(unbanAction, client);
                await _timedActionsHandler.Remove(unbanAction);
            }
            await _timedActionsHandler.Save();
        }

        private async Task HandleAction(TimedActions action, DiscordSocketClient client)
        {
            var guild = client.Guilds.FirstOrDefault(g => g.Id == action.GuildId);
            if (guild is null)
                return;
            try
            {
                await guild.RemoveBanAsync(action.TargetId);
            }
            catch
            {
                // Ignore exception (thrown when the user is not banned)
            }
        }
    }
}
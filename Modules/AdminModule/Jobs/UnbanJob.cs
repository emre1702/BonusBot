using BonusBot.Common.Extensions;
using BonusBot.Common.Interfaces.Services;
using BonusBot.Common.Workers;
using BonusBot.Database;
using BonusBot.Database.Entities.Cases;
using Discord.WebSocket;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BonusBot.AdminModule.Jobs
{
    public class UnbanJob : JobBase
    {
        private readonly BonusDbContext _bonusDbContext;
        private readonly SemaphoreSlim _semaphoreSlim = new(1, 1);
        private readonly IDiscordClientHandler _discordClientHandler;

        public UnbanJob(BonusDbContextFactory bonusDbContextFactory, IDiscordClientHandler discordClientHandler)
            => (_bonusDbContext, _discordClientHandler) = (bonusDbContextFactory.CreateDbContext(), discordClientHandler);

        protected override TimeSpan DelayTime => TimeSpan.FromMinutes(1);

        protected override async ValueTask DoWork()
        {
            await _semaphoreSlim.WaitAsync();
            try
            {
                var client = await _discordClientHandler.ClientSource.Task;
                var unbanActions = await GetActions();
                foreach (var unbanAction in unbanActions)
                {
                    await HandleAction(unbanAction, client);
                    _bonusDbContext.TimedActions.Remove(unbanAction);
                }
                await _bonusDbContext.SaveChangesAsync();
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        private Task<List<TimedActions>> GetActions()
        {
            var currentDate = DateTime.UtcNow;
            var moduleName = GetType().GetModuleName();
            return _bonusDbContext.TimedActions.AsQueryable().Where(a => a.ActionType == ActionType.Unban && a.AtDateTime <= currentDate && a.Module == moduleName).ToListAsync();
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
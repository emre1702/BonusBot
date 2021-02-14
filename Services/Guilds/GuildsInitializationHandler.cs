using Discord.WebSocket;
using BonusBot.Common.Defaults;
using BonusBot.Common.Events.Arguments;
using BonusBot.Common.Helper;
using BonusBot.Database;
using BonusBot.Database.Entities.Settings;
using BonusBot.Services.Events;
using BonusBot.Services.System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BonusBot.Common.Extensions;
using BonusBot.Common;

namespace BonusBot.Services.Guilds
{
    public class GuildsInitializationHandler
    {
        private readonly HashSet<ulong> _guildIdsInitialized = new();
        private readonly SemaphoreSlim _semaphore = new(1, 1);

        private readonly BonusDbContextFactory _dbContextFactory;

        public GuildsInitializationHandler(EventsHandler eventsHandler, BonusDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;

            eventsHandler.GuildAvailable += InitGuild;
        }

        private async Task InitGuild(ClientGuildArg arg)
        {
            await _semaphore.WaitAsync();

            try
            {
                if (_guildIdsInitialized.Contains(arg.Guild.Id))
                    return;

                using var dbContext = _dbContextFactory.CreateDbContext();

                var userName = await dbContext.GuildsSettings.GetString(arg.Guild.Id, CommonSettings.BotName, typeof(CommonSettings).Assembly);

                await arg.Guild.CurrentUser.ModifyAsync(prop =>
                {
                    prop.Nickname = userName ?? Constants.DefaultBotName;
                });
                _guildIdsInitialized.Add(arg.Guild.Id);
                ConsoleHelper.Log(Discord.LogSeverity.Info, Common.Enums.LogSource.Discord, $"Initialized Guild '{arg.Guild.Name}'.");
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
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

                var settings = await dbContext.GuildCoreSettings.FirstOrDefaultAsync(s => s.GuildId == arg.Guild.Id);
                if (settings is null)
                    settings = await CreateGuildSettings(arg.Guild, dbContext);

                await arg.Client.CurrentUser.ModifyAsync(prop =>
                {
                    prop.Username = settings.Name;
                });
                _guildIdsInitialized.Add(arg.Guild.Id);
                ConsoleHelper.Log(Discord.LogSeverity.Info, Common.Enums.LogSource.Discord, $"Initialized Guild '{arg.Guild.Name}'.");
            }
            finally
            {
                _semaphore.Release();
            }
        }

        private async Task<GuildCoreSettings> CreateGuildSettings(SocketGuild guild, BonusDbContext dbContext)
        {
            var botSettings = await dbContext.BotSettings.FirstAsync();

            var settings = new GuildCoreSettings { GuildId = guild.Id, Name = botSettings.DefaultName };
            dbContext.GuildCoreSettings.Add(settings);
            await dbContext.SaveChangesAsync();

            return settings;
        }
    }
}
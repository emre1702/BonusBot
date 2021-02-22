﻿using BonusBot.Common;
using BonusBot.Common.Defaults;
using BonusBot.Common.Extensions;
using BonusBot.Common.Interfaces.Guilds;
using BonusBot.Database;
using Discord.WebSocket;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("BonusBot.Core")]

namespace BonusBot.GuildsSystem
{
    internal class Guild : IBonusGuild
    {
#nullable disable
        public SocketGuild DiscordGuild { get; private set; }
#nullable restore
        public IGuildModulesHandler Modules { get; }
        public IGuildSettingsHandler Settings { get; }

        private readonly BonusDbContextFactory _dbContextFactory;

        public Guild(BonusDbContextFactory dbContextFactory, IGuildModulesHandler guildModulesHandler, IGuildSettingsHandler guildSettingsHandler)
        {
            _dbContextFactory = dbContextFactory;
            Modules = guildModulesHandler;
            Settings = guildSettingsHandler;
        }

        public async Task Initialize(SocketGuild discordGuild)
        {
            DiscordGuild = discordGuild;
            await Settings.Init(discordGuild);

            using var dbContext = _dbContextFactory.CreateDbContext();

            var userName = await dbContext.GuildsSettings.GetString(DiscordGuild.Id, CommonSettings.BotName, typeof(CommonSettings).Assembly);

            await DiscordGuild.CurrentUser.ModifyAsync(prop =>
            {
                prop.Nickname = userName ?? Constants.DefaultBotName;
            });
        }
    }
}
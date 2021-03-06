﻿using BonusBot.Common.Interfaces.Guilds;
using BonusBot.GuildsSystem;
using BonusBot.GuildsSystem.Modules;
using BonusBot.GuildsSystem.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace BonusBot.Core.Services.Guilds
{
    internal static class GuildsProvider
    {
        internal static IServiceCollection WithGuilds(this IServiceCollection serviceCollection)
            => serviceCollection
                .AddSingleton<IGuildsHandler, GuildsHandler>()
                .AddTransient<IGuildSettingsCache, GuildSettingsCache>()
                .AddTransient<IGuildSettingsHandler, GuildSettingsHandler>()
                .AddTransient<IGuildModulesHandler, GuildModulesHandler>();
    }
}
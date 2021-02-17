using BonusBot.Common.Interfaces.Guilds;
using BonusBot.Services.Guilds;
using GuildsSystem.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace BonusBot.Core.Services.Guilds
{
    internal static class GuildsProvider
    {
        internal static IServiceCollection WithGuilds(this IServiceCollection serviceCollection)
            => serviceCollection
                .AddSingleton<GuildsHandler>()
                .AddTransient<IGuildSettingsCache, GuildSettingsCache>()
                .AddTransient<IGuildSettingsHandler, GuildSettingsHandler>()
                .AddSingleton<IBonusGuildProvider, GuildsSystem.Provider>();
    }
}
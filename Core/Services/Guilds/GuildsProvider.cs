using BonusBot.Services.Guilds;
using Microsoft.Extensions.DependencyInjection;

namespace BonusBot.Core.Services.Guilds
{
    internal static class GuildsProvider
    {
        internal static IServiceCollection WithGuilds(this IServiceCollection serviceCollection)
            => serviceCollection.AddSingleton<GuildsInitializationHandler>();
    }
}
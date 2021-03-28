using BonusBot.Common.Interfaces.Services;
using BonusBot.Services.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace BonusBot.Core.Services.Guilds
{
    internal static class SettingsProvider
    {
        internal static IServiceCollection WithSettings(this IServiceCollection serviceCollection)
            => serviceCollection
                .AddSingleton<ISettingsHandler, SettingsHandler>();
    }
}
using BonusBot.Services.System;
using Microsoft.Extensions.DependencyInjection;

namespace BonusBot.Core.Services.System
{
    internal static class SystemProvider
    {
        internal static IServiceCollection WithSystem(this IServiceCollection serviceCollection)
            => serviceCollection
                .AddSingleton<GlobalizationHandler>();
    }
}
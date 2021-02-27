using BonusBot.Common.Interfaces.Services;
using BonusBot.Services.Workers;
using Microsoft.Extensions.DependencyInjection;

namespace BonusBot.Core.Services.Workers
{
    internal static class WorkersProvider
    {
        internal static IServiceCollection WithWorkers(this IServiceCollection serviceCollection)
            => serviceCollection
                .AddSingleton<JobsHandler>()
                .AddSingleton<ITimedActionsHandler, TimedActionsHandler>();
    }
}
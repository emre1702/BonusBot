using BonusBot.Common.Interfaces.Services;
using BonusBot.Services.Events;
using Microsoft.Extensions.DependencyInjection;

namespace BonusBot.Core.Services.Events
{
    internal static class EventsHandlerProvider
    {
        internal static IServiceCollection WithEventsHandler(this IServiceCollection serviceCollection)
            => serviceCollection.AddSingleton<IEventsHandler, EventsHandler>();
    }
}
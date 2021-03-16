using BonusBot.Common.Interfaces.Services;
using BonusBot.Services.DiscordNet;
using BonusBot.WebDashboardBoardModule.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BonusBot.Core.Services.DiscordNet
{
    internal static class DiscordNetProvider
    {
        internal static IServiceCollection WithDiscordNet(this IServiceCollection serviceCollection)
            => serviceCollection
                .AddSingleton<IDiscordClientHandler, SocketClientHandler>()
                .AddSingleton<IModulesHandler, ModulesHandler>()
                .AddSingleton<CommandsHandler>();
    }
}
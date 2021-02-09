﻿using Discord.Commands;
using BonusBot.Services.DiscordNet;
using Microsoft.Extensions.DependencyInjection;

namespace BonusBot.Core.Services.DiscordNet
{
    internal static class DiscordNetProvider
    {
        internal static IServiceCollection WithDiscordNet(this IServiceCollection serviceCollection)
            => serviceCollection
                .AddSingleton<SocketClientHandler>()
                .AddSingleton<ModulesHandler>()
                .AddSingleton<CommandsHandler>();
    }
}
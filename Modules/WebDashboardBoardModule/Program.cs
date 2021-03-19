using BonusBot.Common.Extensions;
using BonusBot.Common.Interfaces.Core;
using BonusBot.Common.Interfaces.Guilds;
using BonusBot.Common.Interfaces.Services;
using Discord.Commands;
using Discord.Commands.Builders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace WebDashboardBoard
{
    public class Program : CommandBase
    {
        private readonly ICustomServiceProvider _mainServiceProvider;

        public Program(ICustomServiceProvider mainServiceProvider)
            => _mainServiceProvider = mainServiceProvider;

        public static void Main()
        {
            throw new InvalidOperationException("Only start from BonusBot.Core!");
        }

        protected override void OnModuleBuilding(CommandService commandService, ModuleBuilder builder)
        {
            base.OnModuleBuilding(commandService, builder);

            CreateHostBuilder().Build().RunAsync();
        }

        public IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureServices(services =>
                    {
                        services.AddSingleton(provider => _mainServiceProvider.GetRequiredService<IDiscordClientHandler>());
                        services.AddSingleton(provider => _mainServiceProvider.GetRequiredService<IModulesHandler>());
                        services.AddSingleton(provider => _mainServiceProvider.GetRequiredService<IGuildsHandler>());
                        services.AddSingleton(_mainServiceProvider.GetRequiredService<ICommandsHandler>());
                        services.AddSingleton(_mainServiceProvider);
                    });
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureKestrel(option =>
                    {
                        option.ListenAnyIP(26457);
                    });

                });
    }
}

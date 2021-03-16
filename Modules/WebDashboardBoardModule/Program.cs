using BonusBot.Common.Extensions;
using BonusBot.WebDashboardBoardModule.Services;
using Discord.Commands;
using Discord.Commands.Builders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.Extensions.DependencyInjection;
using BonusBot.Common.Interfaces.Services;
using BonusBot.Common.Interfaces.Guilds;

namespace WebDashboardBoard
{
    public class Program : CommandBase
    {
        private readonly IServiceProvider _mainServiceProvider;

        public Program(IServiceProvider mainServiceProvider)
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
                    });
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureKestrel(option =>
                    {
                        option.ListenAnyIP(26457);
                    });
                    
                });
    }
}

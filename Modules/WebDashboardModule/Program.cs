using BonusBot.Common.Extensions;
using BonusBot.Common.Interfaces.Core;
using BonusBot.Common.Interfaces.Guilds;
using BonusBot.Common.Interfaces.Services;
using BonusBot.WebDashboardModule.Services;
using Discord.Commands;
using Discord.Commands.Builders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace BonusBot.WebDashboardModule
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
                        services.AddSingleton(_mainServiceProvider.GetRequiredService<IDiscordClientHandler>());
                        services.AddSingleton(_mainServiceProvider.GetRequiredService<IModulesHandler>());
                        services.AddSingleton(_mainServiceProvider.GetRequiredService<IGuildsHandler>());
                        services.AddSingleton(_mainServiceProvider.GetRequiredService<ICommandsHandler>());
                        services.AddSingleton(_mainServiceProvider.GetRequiredService<ISettingsHandler>());
                        services.AddSingleton(_mainServiceProvider);
                       
                        services.AddSingleton<ContentService>();
                        services.AddSingleton<ContextProvideService>();
                        services.AddSingleton<NavigationService>();
                        services.AddSingleton<SettingsService>();
                        services.AddSingleton<TokenRequestService>();
                        services.AddSingleton<UserRequestService>();
                        services.AddSingleton<UserValidationService>();
                        services.AddScoped<WebCommandService>();
                    });
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureKestrel(option =>
                    {
                        option.ListenAnyIP(26457);
                    });

                });
    }
}

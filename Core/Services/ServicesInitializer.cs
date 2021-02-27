using BonusBot.Core.Services.Database;
using BonusBot.Core.Services.DiscordNet;
using BonusBot.Core.Services.Events;
using BonusBot.Core.Services.Guilds;
using BonusBot.Core.Services.Workers;
using BonusBot.Services.Database;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace BonusBot.Core.Services
{
    internal class ServicesInitializer
    {
        internal static async Task<CustomServiceProvider> InitializeAsync()
        {
            var databaseInitializationHandler = new DatabaseInitializationHandler();
            await databaseInitializationHandler.InitializeAsync();

            var serviceCollection = new ServiceCollection();
            serviceCollection
                .WithDatabase()
                .WithDiscordNet()
                .WithEventsHandler()
                .WithGuilds()
                .WithWorkers();
            return new CustomServiceProvider(serviceCollection);
        }
    }
}
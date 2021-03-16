using BonusBot.Common.Helper;
using BonusBot.Common.Interfaces.Services;
using BonusBot.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Core
{
    internal class Startup
    {
        private static Task Main()
        {
            return new Startup().InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            ConsoleHelper.PrintHeader();
            var serviceProvider = await ServicesInitializer.InitializeAsync();
            serviceProvider.InitAllSingletons();

            await serviceProvider.GetRequiredService<IModulesHandler>().LoadModules();

            await Task.Delay(-1);
        }
    }
}
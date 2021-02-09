using BonusBot.Common.Helper;
using BonusBot.Core.Services;
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

            await Task.Delay(-1);
        }
    }
}
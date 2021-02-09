using BonusBot.Database;
using Microsoft.Extensions.DependencyInjection;

namespace BonusBot.Core.Services.Database
{
    internal static class DatabaseProvider
    {
        internal static IServiceCollection WithDatabase(this IServiceCollection serviceCollection)
            => serviceCollection.AddSingleton<FunDbContextFactory>();
    }
}
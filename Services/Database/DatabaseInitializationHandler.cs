using BonusBot.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("BonusBot.Core")]

namespace BonusBot.Services.Database
{
    internal class DatabaseInitializationHandler
    {
        public async Task InitializeAsync()
        {
            using var dbContext = new BonusDbContextFactory().CreateDbContext(Array.Empty<string>());

            await dbContext.Database.MigrateAsync();
        }
    }
}
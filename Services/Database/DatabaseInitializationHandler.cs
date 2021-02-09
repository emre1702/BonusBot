using BonusBot.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BonusBot.Services.Database
{
    public class DatabaseInitializationHandler
    {
        public async Task InitializeAsync()
        {
            using var dbContext = new FunDbContextFactory().CreateDbContext(Array.Empty<string>());

            await dbContext.Database.MigrateAsync();
        }
    }
}
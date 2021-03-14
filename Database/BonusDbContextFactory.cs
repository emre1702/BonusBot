using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Runtime.InteropServices;

namespace BonusBot.Database
{
    public class BonusDbContextFactory : IDesignTimeDbContextFactory<BonusDbContext>
    {
        private string ConnectionString => RuntimeInformation.IsOSPlatform(OSPlatform.Windows) 
            ? "Data Source=BonusBot.db;" 
            : "Data Source=/bonusbot-data/BonusBot.db;";

        public BonusDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BonusDbContext>();
            optionsBuilder.UseSqlite(ConnectionString);

            return new BonusDbContext(optionsBuilder.Options);
        }

        public BonusDbContext CreateDbContext() => CreateDbContext(Array.Empty<string>());
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace BonusBot.Database
{
    public class BonusDbContextFactory : IDesignTimeDbContextFactory<BonusDbContext>
    {
        private const string _connectionString = "Data Source=BonusBot.db;";

        public BonusDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BonusDbContext>();
            optionsBuilder.UseSqlite(_connectionString);

            return new BonusDbContext(optionsBuilder.Options);
        }

        public BonusDbContext CreateDbContext() => CreateDbContext(Array.Empty<string>());
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace BonusBot.Database
{
    public class FunDbContextFactory : IDesignTimeDbContextFactory<FunDbContext>
    {
        private const string _connectionString = "Data Source=BonusBot.db;";

        public FunDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FunDbContext>();
            optionsBuilder.UseSqlite(_connectionString);

            return new FunDbContext(optionsBuilder.Options);
        }

        public FunDbContext CreateDbContext() => CreateDbContext(Array.Empty<string>());
    }
}
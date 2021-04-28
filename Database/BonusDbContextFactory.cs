using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Runtime.InteropServices;

namespace BonusBot.Database
{
    public class BonusDbContextFactory : IDesignTimeDbContextFactory<BonusDbContext>
    {
        private string ConnectionString => Environment.GetEnvironmentVariable("BONUSBOT_CONNECTION_STRING") ?? "Host=postgres;Database=database;Username=user;Password=password;Timeout=100;Command Timeout = 100";

        public BonusDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BonusDbContext>();
            optionsBuilder.UseNpgsql(ConnectionString);

            return new BonusDbContext(optionsBuilder.Options);
        }

        public BonusDbContext CreateDbContext() => CreateDbContext(Array.Empty<string>());
    }
}
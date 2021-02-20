using BonusBot.Database.Entities.Cases;
using BonusBot.Database.Entities.Logging;
using BonusBot.Database.Entities.Settings;
using BonusBot.Database.Seeds;
using Microsoft.EntityFrameworkCore;
using System;

namespace BonusBot.Database
{
#nullable disable

    public class BonusDbContext : DbContext
    {
        public BonusDbContext(DbContextOptions options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<GuildsSettings> GuildsSettings { get; set; }
        public DbSet<Logs> Logs { get; set; }
        public DbSet<TimedActions> TimedActions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .HasSeeds()
                .ApplyConfigurationsFromAssembly(typeof(BonusDbContext).Assembly, IsNotBaseConfiguration);
        }

        private bool IsNotBaseConfiguration(Type type)
            => !type.Name.EndsWith("BaseConfiguration");
    }
}
using BonusBot.Database.Entities.Settings;
using BonusBot.Database.Seeds;
using Microsoft.EntityFrameworkCore;

namespace BonusBot.Database
{
#nullable disable

    public class BonusDbContext : DbContext
    {
        public BonusDbContext(DbContextOptions options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<BotSettings> BotSettings { get; set; }
        public DbSet<GuildsSettings> GuildsSettings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .HasSeeds()
                .ApplyConfigurationsFromAssembly(typeof(BonusDbContext).Assembly);
        }
    }
}
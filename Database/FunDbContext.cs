using BonusBot.Database.Entities.Settings;
using BonusBot.Database.Seeds;
using Microsoft.EntityFrameworkCore;

namespace BonusBot.Database
{
#nullable disable

    public class FunDbContext : DbContext
    {
        public FunDbContext(DbContextOptions options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<BotSettings> BotSettings { get; set; }
        public DbSet<GuildCoreSettings> GuildCoreSettings { get; set; }
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
                .ApplyConfigurationsFromAssembly(typeof(FunDbContext).Assembly);
        }
    }
}
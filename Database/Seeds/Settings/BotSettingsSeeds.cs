using BonusBot.Database.Entities.Settings;
using Microsoft.EntityFrameworkCore;

namespace BonusBot.Database.Seeds.Settings
{
    internal static class BotSettingsSeeds
    {
        internal static ModelBuilder HasBotSettings(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BotSettings>().HasData(
                new BotSettings()
            );
            return modelBuilder;
        }
    }
}
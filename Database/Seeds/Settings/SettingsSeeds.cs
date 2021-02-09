using Microsoft.EntityFrameworkCore;

namespace BonusBot.Database.Seeds.Settings
{
    internal static class SettingsSeeds
    {
        internal static ModelBuilder HasSettings(this ModelBuilder modelBuilder)
            => modelBuilder.HasBotSettings();
    }
}
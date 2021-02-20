using BonusBot.Database.Seeds.Settings;
using Microsoft.EntityFrameworkCore;

namespace BonusBot.Database.Seeds
{
    internal static class SeedsInitializer
    {
        internal static ModelBuilder HasSeeds(this ModelBuilder modelBuilder)
            => modelBuilder;
    }
}
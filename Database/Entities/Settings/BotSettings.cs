using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BonusBot.Database.Entities.Settings
{
    public class BotSettings
    {
        public int Id { get; set; } = -1;
        public string DefaultName { get; set; } = "BonusBot";
    }

    public class BotSettingsConfiguration : IEntityTypeConfiguration<BotSettings>
    {
        public void Configure(EntityTypeBuilder<BotSettings> builder)
        {
        }
    }
}
using BonusBot.Database.Entities.Bases;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BonusBot.Database.Entities.Settings
{
    public class GuildsSettings : ModuleEntityBase
    {
        public string Key { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }

    public class GuildsSettingsConfiguration : ModuleEntityBaseConfiguration<GuildsSettings>
    {
        public override void Configure(EntityTypeBuilder<GuildsSettings> builder)
        {
            base.Configure(builder);

            builder.HasKey(e => new { e.GuildId, e.Key, e.Module });
        }
    }
}
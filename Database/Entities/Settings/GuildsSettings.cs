using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BonusBot.Database.Entities.Settings
{
    public class GuildsSettings
    {
        public ulong GuildId { get; set; }
        public string Key { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string Module { get; set; } = string.Empty;
    }

    public class GuildsSettingsConfiguration : IEntityTypeConfiguration<GuildsSettings>
    {
        public void Configure(EntityTypeBuilder<GuildsSettings> builder)
        {
            builder.HasKey(e => new { e.GuildId, e.Key, e.Module });

            builder.Property(e => e.GuildId).ValueGeneratedNever();
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BonusBot.Database.Entities.Settings
{
    public class GuildCoreSettings
    {
        public ulong GuildId { get; set; }
        public string Name { get; set; } = "BonusBot";
        public string CommandPrefix { get; set; } = "!";
        public bool CommandMentionAllowed { get; set; } = true;
    }

    public class GuildCoreSettingsConfiguration : IEntityTypeConfiguration<GuildCoreSettings>
    {
        public void Configure(EntityTypeBuilder<GuildCoreSettings> builder)
        {
            builder.HasKey(e => e.GuildId);

            builder.Property(e => e.GuildId).ValueGeneratedNever();
        }
    }
}
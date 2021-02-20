using BonusBot.Database.Entities.Bases;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BonusBot.Database.Entities.Logging
{
    public class Logs : ModuleTimeEntityBase
    {
        public int Id { get; set; }
        public ulong TargetId { get; set; }
        public ulong SourceId { get; set; }
        public string Type { get; set; } = string.Empty;
    }

    public class LogsConfiguration : ModuleTimeEntityBaseConfiguration<Logs>
    {
        public override void Configure(EntityTypeBuilder<Logs> builder)
        {
            base.Configure(builder);

            builder
                .HasIndex(e => new { e.GuildId, e.Module, e.Type, e.TargetId })
                .IsUnique(false);
        }
    }
}
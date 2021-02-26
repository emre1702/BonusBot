using BonusBot.Database.Entities.Bases;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace BonusBot.Database.Entities.Cases
{
    public class TimedActions : ModuleTimeEntityBase
    {
        public int Id { get; set; }
        public ulong TargetId { get; set; }
        public ulong SourceId { get; set; }
        public string ActionType { get; set; } = string.Empty;
        public DateTime AtDateTime { get; set; }
        public TimeSpan? MaxDelay { get; set; }
    }

    public class TimedActionsConfiguration : ModuleTimeEntityBaseConfiguration<TimedActions>
    {
        public override void Configure(EntityTypeBuilder<TimedActions> builder)
        {
            base.Configure(builder);

            builder
                .HasIndex(e => new { e.GuildId, e.Module, e.ActionType, e.TargetId })
                .IsUnique(false);
        }
    }
}
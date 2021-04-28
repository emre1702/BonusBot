using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace BonusBot.Database.Entities.Bases
{
    public class ModuleTimeEntityBase : ModuleEntityBase
    {
        public DateTime AddedDateTime { get; set; }
    }

    public class ModuleTimeEntityBaseConfiguration<TEntity> : ModuleEntityBaseConfiguration<TEntity> where TEntity : ModuleTimeEntityBase
    {
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            base.Configure(builder);

            builder
                .Property(e => e.AddedDateTime)
                .HasDefaultValue(DateTime.UtcNow)
                .HasConversion(
                    fromCode => fromCode.ToUniversalTime(),
                    fromDb => DateTime.SpecifyKind(fromDb, DateTimeKind.Utc)
                );
        }
    }
}
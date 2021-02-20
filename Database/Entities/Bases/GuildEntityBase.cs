using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BonusBot.Database.Entities.Bases
{
    public class GuildEntityBase
    {
        public ulong GuildId { get; set; }
    }

    public class GuildEntityBaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : GuildEntityBase
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder
                .Property(e => e.GuildId)
                .ValueGeneratedNever();
        }
    }
}
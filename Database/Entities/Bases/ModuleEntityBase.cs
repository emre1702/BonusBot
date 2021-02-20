using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BonusBot.Database.Entities.Bases
{
    public class ModuleEntityBase : GuildEntityBase
    {
        public string Module { get; set; } = string.Empty;
    }

    public class ModuleEntityBaseConfiguration<TEntity> : GuildEntityBaseConfiguration<TEntity> where TEntity : ModuleEntityBase
    {
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            base.Configure(builder);
        }
    }
}
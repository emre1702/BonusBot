using BonusBot.Database.Entities.Settings;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BonusBot.Common.Extensions
{
    public static class GuildsSettingsExtensions
    {
        public static Task<GuildsSettings?> Get(this DbSet<GuildsSettings> dbSet, ulong guildId, string key, string moduleName)
        {
            moduleName = moduleName.ToModuleName();
            return dbSet.FirstOrDefaultAsync(s =>
               s.GuildId == guildId &&
               EF.Functions.Like(s.Key, key) &&
               EF.Functions.Like(s.Module, moduleName)) as Task<GuildsSettings?>;
        }

        public static GuildsSettings Create(this DbSet<GuildsSettings> dbSet, ulong guildId, string key, string moduleName, string value)
        {
            var settings = new GuildsSettings { GuildId = guildId, Module = moduleName, Key = key, Value = value };
            dbSet.Add(settings);
            return settings;
        }
    }
}
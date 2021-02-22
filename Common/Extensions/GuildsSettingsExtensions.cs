using BonusBot.Database.Entities.Settings;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
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
            moduleName = moduleName.ToModuleName();
            var settings = new GuildsSettings { GuildId = guildId, Module = moduleName, Key = key, Value = value };
            dbSet.Add(settings);
            return settings;
        }

        public static async Task<GuildsSettings> GetOrCreate(this DbSet<GuildsSettings> dbSet, ulong guildId, string key, string moduleName)
        {
            var setting = await Get(dbSet, guildId, key, moduleName);
            if (setting is null)
                setting = Create(dbSet, guildId, key, moduleName, string.Empty);
            return setting;
        }

        public static async Task<GuildsSettings> GetOrCreate(this DbSet<GuildsSettings> dbSet, DbContext dbContext, ulong guildId, string key, string moduleName, object defaultValue)
        {
            var setting = await Get(dbSet, guildId, key, moduleName);
            if (setting is null)
            {
                setting = Create(dbSet, guildId, key, moduleName, defaultValue.ToString() ?? string.Empty);
                await dbContext.SaveChangesAsync();
            }

            return setting;
        }

        public static async Task<string?> GetOrCreateString(this DbSet<GuildsSettings> dbSet, DbContext dbContext, ulong guildId, string key, string moduleName, object defaultValue)
        {
            var setting = await GetOrCreate(dbSet, dbContext, guildId, key, moduleName, defaultValue);
            return setting.Value;
        }

        public static Task<string?> GetOrCreateString(this DbSet<GuildsSettings> dbSet, DbContext dbContext, ulong guildId, string key, Assembly moduleAssembly, object defaultValue)
            => GetOrCreateString(dbSet, dbContext, guildId, key, moduleAssembly.ToModuleName(), defaultValue);

        public static async Task<GuildsSettings> AddOrUpdate(this DbSet<GuildsSettings> dbSet, ulong guildId, string key, string moduleName, string value)
        {
            var setting = await Get(dbSet, guildId, key, moduleName);
            if (setting is null)
                setting = Create(dbSet, guildId, key, moduleName, string.Empty);
            setting.Value = value;
            return setting;
        }

        public static Task<GuildsSettings> AddOrUpdate(this DbSet<GuildsSettings> dbSet, ulong guildId, string key, Assembly moduleAssembly, string value)
            => AddOrUpdate(dbSet, guildId, key, moduleAssembly.ToModuleName(), value);

        public static async Task<bool> Remove(this DbSet<GuildsSettings> dbSet, ulong guildId, string key, string moduleName)
        {
            var setting = await Get(dbSet, guildId, key, moduleName);
            if (setting is null)
                return false;
            dbSet.Remove(setting);
            return true;
        }
    }
}
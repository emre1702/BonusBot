using BonusBot.Database.Entities.Cases;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Threading.Tasks;

namespace BonusBot.Common.Extensions
{
    public static class TimedActionsExtensions
    {
        public static Task<TimedActions?> Get(this DbSet<TimedActions> dbSet, ulong guildId, string actionType, ulong targetId)
        {
            var callerAssembly = Assembly.GetCallingAssembly();
            var moduleName = callerAssembly.ToModuleName();

            return dbSet.FirstOrDefaultAsync(s =>
               s.GuildId == guildId &&
               EF.Functions.Like(s.Module, moduleName) &&
               EF.Functions.Like(s.ActionType, actionType) &&
               s.TargetId == targetId
            ) as Task<TimedActions?>;
        }
    }
}
using BonusBot.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BonusBot.SqliteToPostgresModule
{
    internal class DbDataCopyHandler
    {
        public async Task CopyDatas(BonusDbContext from, BonusDbContext to)
        {
            var dbSets = GetAllDbSets();
            
            var allDbSets = dbSets.Select(dbSet => dbSet.GetValue(from)).OfType<IQueryable<object>>();
            foreach (var dbSet in allDbSets)
                to.AddRange(await dbSet.ToListAsync());
            await to.SaveChangesAsync();
        }

        private IEnumerable<PropertyInfo> GetAllDbSets()
            => typeof(BonusDbContext)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(prop => prop.PropertyType.IsGenericType && typeof(DbSet<>).IsAssignableFrom(prop.PropertyType.GetGenericTypeDefinition()));
    }
}

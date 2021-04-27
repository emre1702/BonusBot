using BonusBot.Database;
using BonusBot.Database.Entities.Cases;
using BonusBot.Database.Entities.Logging;
using BonusBot.Database.Entities.Settings;
using BonusBot.SqliteToPostgresModule;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;

namespace BonusBot.Tests.Modules.SqliteToPostgresModuleTests
{
    public class DbDataCopyHandlerTests
    {
        [Test]
        public async Task Copies_All_Rows_Between_DbContexts()
        {
            using var from = await GetDbContext();
            using var to = await GetDbContext();
            
            await AddData(from);

            Assert.AreNotEqual(await from.Logs.CountAsync(), await to.Logs.CountAsync());

            var dbDataCopyHandler = new DbDataCopyHandler();
            await dbDataCopyHandler.CopyDatas(from, to);

            Assert.AreEqual(await from.Logs.CountAsync(), 3);
            Assert.AreEqual(await from.GuildsSettings.FirstAsync(), await to.GuildsSettings.FirstAsync());
            Assert.AreEqual(await from.Logs.CountAsync(), await to.Logs.CountAsync());
            Assert.AreEqual((await from.TimedActions.FirstAsync()).Id, (await to.TimedActions.FirstAsync()).Id);
        }

        private async Task<BonusDbContext> GetDbContext()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            
            var options = new DbContextOptionsBuilder<BonusDbContext>()
                .UseSqlite(connection)
                .Options;
            var dbContext = new BonusDbContext(options);
            await dbContext.Database.MigrateAsync();
            return dbContext; 
        }

        private async Task AddData(BonusDbContext dbContext)
        {
            dbContext.GuildsSettings.Add(new GuildsSettings { GuildId = 0, Key = "Key", Module = "Module", Value = "Value" });
            dbContext.Logs.AddRange(new Logs { Id = 1 }, new Logs { Id = 2 }, new Logs { Id = 3 });
            dbContext.TimedActions.Add(new TimedActions { Id = 5 });
            await dbContext.SaveChangesAsync();
        }
    }
}
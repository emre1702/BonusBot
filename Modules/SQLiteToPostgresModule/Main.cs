using BonusBot.Common.Commands.Conditions;
using BonusBot.Common.Extensions;
using BonusBot.Database;
using BonusBot.SqliteToPostgresModule;
using BonusBot.SqliteToPostgresModule.Language;
using Discord.Commands;
using Discord.Commands.Builders;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace SQLiteToPostgresModule
{
    [RequireNotDisabledInGuild(typeof(Main))]
    [RequireContext(ContextType.DM)]
    public class Main : CommandBase
    {
        [Command("ConvertSqliteToPostgres")]
        public async Task ConvertSqliteToPostgres(string sqliteConnectionString, string postgresConnectionString)
        {
            using var sqliteDbContext = GetSqliteDbContext(sqliteConnectionString);
            using var postgresDbContext = GetPostgresDbContext(postgresConnectionString);

            if (!await sqliteDbContext.Database.CanConnectAsync())
                await ReplyErrorAsync(string.Format(ModuleTexts.ConnectionStringInvalid, "Sqlite", sqliteConnectionString));
            if (!await postgresDbContext.Database.CanConnectAsync())
                await ReplyErrorAsync(string.Format(ModuleTexts.ConnectionStringInvalid, "Postgres", postgresConnectionString));

            await postgresDbContext.Database.MigrateAsync();

            var copyHandler = new DbDataCopyHandler();
            await copyHandler.CopyDatas(sqliteDbContext, postgresDbContext);
        }

        private BonusDbContext GetSqliteDbContext(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BonusDbContext>();
            optionsBuilder.UseSqlite(connectionString);

            return new BonusDbContext(optionsBuilder.Options);
        }

        private BonusDbContext GetPostgresDbContext(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BonusDbContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new BonusDbContext(optionsBuilder.Options);
        }
    }
}

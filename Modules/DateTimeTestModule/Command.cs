using BonusBot.Common;
using BonusBot.Common.Extensions;
using BonusBot.Database;
using BonusBot.Database.Entities.Cases;
using BonusBot.DateTimeTestModule.Language;
using Discord.Commands;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonusBot.DateTimeTestModule
{
    [Group("datetest")]
    [Alias("datetimetest")]
    public class Command : CommandBase
    {
        private readonly BonusDbContextFactory _bonusDbFactory;

        public Command(BonusDbContextFactory bonusDbFactory)
            => _bonusDbFactory = bonusDbFactory;

        [Command("output")]
        [Priority(1)]
        public async Task Output()
        {
            var time = DateTime.Now.ToString();
            var localTime = DateTime.Now.ToLocalTime().ToString();
            var utcTime = DateTime.Now.ToUniversalTime().ToString();
            var guildTimeZone = Context.BonusGuild is not null ? await Context.BonusGuild.Settings.Get<string>(typeof(CommonSettings).Assembly, CommonSettings.TimeZone) : "-";
            var timeZone = TimeZoneInfo.Local.DisplayName;
            var daylightSaving = TimeZoneInfo.Local.DaylightName;
            var utcOffset = TimeZoneInfo.Local.BaseUtcOffset;

            var msg = string.Format(ModuleTexts.OutputLocalTimeInfo, time, localTime, utcTime, guildTimeZone, timeZone, daylightSaving, utcOffset);
            await ReplyToUserAsync(msg);
        }

        [Command("output")]
        [Priority(2)]
        public async Task Output(DateTimeOffset dateTimeOffset)
        {
            var time = dateTimeOffset.ToString();
            var localTime = dateTimeOffset.ToLocalTime().ToString();
            var localDateTime = dateTimeOffset.LocalDateTime.ToString();
            var utcTime = dateTimeOffset.ToUniversalTime().ToString();
            var utcDateTime = dateTimeOffset.UtcDateTime.ToString();
            var isDaylightSaving = TimeZoneInfo.Local.IsDaylightSavingTime(dateTimeOffset);
            var offset = dateTimeOffset.Offset.ToString();

            var msg1 = string.Format(ModuleTexts.OutputParsedDateTimeOffsetInfo, time, localTime, localDateTime, utcTime, utcDateTime, isDaylightSaving, offset);
            await ReplyToUserAsync(msg1);

            var guid = Guid.NewGuid();
            await AddToDb(dateTimeOffset, guid);
            var dateTimeInDb = await GetFromDatabase(guid);
            var dbDateTime = dateTimeInDb.ToString();
            var dbDateTimeLocal = dateTimeInDb.ToLocalTime().ToString();
            var dbDateTimeUtc = dateTimeInDb.ToUniversalTime().ToString();
            var isDbDateTimeDaylightSaving = TimeZoneInfo.Local.IsDaylightSavingTime(dateTimeInDb);

            var msg2 = string.Format(ModuleTexts.OutputParsedDbDateTimeOffsetInfo, dbDateTime, dbDateTimeLocal, dbDateTimeUtc, isDbDateTimeDaylightSaving);
            await ReplyToUserAsync(msg2);
        }

        private async Task AddToDb(DateTimeOffset dateTimeOffset, Guid guid)
        {
            await using var dbContext = _bonusDbFactory.CreateDbContext();
            var action = new TimedActions
            {
                 ActionType = guid.ToString(),
                 AddedDateTime = dateTimeOffset.UtcDateTime,
                 AtDateTime = dateTimeOffset.UtcDateTime.AddYears(1000),
                 GuildId = 0,
                 Module = "-",
                 SourceId = 0,
                 TargetId = 0
            };
            dbContext.TimedActions.Add(action);
            await dbContext.SaveChangesAsync();
        }

        private async Task<DateTime> GetFromDatabase(Guid guid)
        {
            await using var dbContext = _bonusDbFactory.CreateDbContext();
            var action = await dbContext.TimedActions.FirstAsync(a => a.ActionType == guid.ToString());
            dbContext.Remove(action);
            await dbContext.SaveChangesAsync();
            return action.AddedDateTime;
        }
    }
}

using BonusBot.Common.Interfaces.Commands;
using BonusBot.Common.Interfaces.Guilds;
using BonusBot.Common.Languages;
using Discord.Commands;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace BonusBot.Common.Commands.TypeReaders
{
    public class DateTimeOffsetTypeReader : TypeReader
    {
        public override async Task<TypeReaderResult> ReadAsync(ICommandContext context, string input, IServiceProvider _)
        {
            var cultureInfo = await GetDateTimeInputCulture((ICustomCommandContext)context);
            if (!DateTime.TryParse(input, cultureInfo, DateTimeStyles.AllowWhiteSpaces, out var dateTime))
                return TypeReaderResult.FromError(CommandError.ParseFailed, string.Format(Texts.CommandInvalidDateTimeOffsetError, input));

            var hasTimeZoneInfo = HasTimeZoneInfo(dateTime);
            dateTime = dateTime.ToUniversalTime();
            var (result, timeZone) = await GetDateTimeWithGuildTimezone((ICustomCommandContext)context, dateTime, hasTimeZoneInfo);
            if (result is null)
                return TypeReaderResult.FromError(CommandError.Unsuccessful, string.Format(Texts.InvalidTimeZoneIdError, timeZone));
            return TypeReaderResult.FromSuccess(result);
        }

        private async Task<(DateTimeOffset?, string)> GetDateTimeWithGuildTimezone(ICustomCommandContext context, DateTime dateTime, bool hasTimeZoneInfo)
        {
            var (utcOffset, timeZoneName) = await GetTimeZoneOffset(context, dateTime);

            if (hasTimeZoneInfo)
                dateTime = dateTime.AddMinutes(utcOffset.TotalMinutes);
            else 
                dateTime = dateTime.AddMinutes(TimeZoneInfo.Local.GetUtcOffset(dateTime).TotalMinutes);
            dateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Unspecified);
            var dateTimeOffset = new DateTimeOffset(dateTime, utcOffset);
            return (dateTimeOffset, timeZoneName);
        }

        private async ValueTask<(TimeSpan, string)> GetTimeZoneOffset(ICustomCommandContext context, DateTime dateTime)
            => await GetGuildTimeZoneOffset(context.BonusGuild)
                ??  (HasTimeZoneInfo(dateTime) ? TimeZoneInfo.Local.GetUtcOffset(dateTime) : DateTimeOffset.Now.Offset, "?");

        private async ValueTask<(TimeSpan timeZoneOffset, string timeZoneSetting)?> GetGuildTimeZoneOffset(IBonusGuild? guild)
        {
            if (guild is null) return null;

            var timeZoneSetting = await guild.Settings.Get<string>(typeof(CommonSettings).Assembly, CommonSettings.TimeZone);
            if (timeZoneSetting is null) return null;
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneSetting);
            if (timeZoneInfo is null) return null;

            var timeZoneOffset = timeZoneInfo.GetUtcOffset(DateTime.Now);

            return (timeZoneOffset, timeZoneSetting!);
        }

        private bool HasTimeZoneInfo(DateTime dateTime)
            => dateTime.Kind == DateTimeKind.Local;

        private async ValueTask<CultureInfo> GetDateTimeInputCulture(ICustomCommandContext context)
            => await GetGuildDateTimeInputCulture(context.BonusGuild) ?? CultureInfo.InvariantCulture;

        private async ValueTask<CultureInfo?> GetGuildDateTimeInputCulture(IBonusGuild? guild)
        {
            if (guild is null) return null;

            var locale = await guild.Settings.Get<string>(typeof(CommonSettings).Assembly, CommonSettings.Locale);
            if (locale is null) return null;

            return new CultureInfo(locale);
        }
    }
}
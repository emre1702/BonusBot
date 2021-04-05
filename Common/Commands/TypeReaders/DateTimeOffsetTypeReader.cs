using BonusBot.Common.Interfaces.Commands;
using BonusBot.Common.Languages;
using Discord.Commands;
using System;
using System.Threading.Tasks;
using TimeZoneConverter;

namespace BonusBot.Common.Commands.TypeReaders
{
    public class DateTimeOffsetTypeReader : TypeReader
    {
        public override async Task<TypeReaderResult> ReadAsync(ICommandContext context, string input, IServiceProvider _)
        {
            if (!DateTime.TryParse(input, out var dateTime))
                return TypeReaderResult.FromError(CommandError.ParseFailed, string.Format(Texts.CommandInvalidDateTimeOffsetError, input));

            bool hasTimeZoneInfo = HasTimeZoneInfo(dateTime);
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
        {
            if (context.BonusGuild is { })
            {
                var timeZoneSetting = await context.BonusGuild.Settings.Get<string>(typeof(CommonSettings).Assembly, CommonSettings.TimeZone);
                if (timeZoneSetting is { })
                {
                    if (TZConvert.TryGetTimeZoneInfo(timeZoneSetting, out var timeZoneInfo))
                        return (timeZoneInfo.GetUtcOffset(DateTime.Now), timeZoneSetting);
                }
                    
            }
            return (HasTimeZoneInfo(dateTime) ? TimeZoneInfo.Local.GetUtcOffset(dateTime) : TimeSpan.Zero, "?");
        }

        private bool HasTimeZoneInfo(DateTime dateTime)
            => dateTime.Kind == DateTimeKind.Local;
    }
}
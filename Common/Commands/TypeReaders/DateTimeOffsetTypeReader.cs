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
        public override async Task<TypeReaderResult> ReadAsync(ICommandContext context, string input, IServiceProvider services)
        {
            if (!DateTime.TryParse(input, out var dateTime))
                return TypeReaderResult.FromError(CommandError.ParseFailed, string.Format(Texts.CommandInvalidDateTimeOffsetError, input));

            if (DoesDateTimeHasGivenTimezone(dateTime))
            {
                var offsetWithGivenTimezone = new DateTimeOffset(dateTime);
                return TypeReaderResult.FromSuccess(offsetWithGivenTimezone);
            }

            var (result, timeZone) = await GetDateTimeWithGuildTimezone(dateTime, (ICustomCommandContext)context);
            if (result is null)
                return TypeReaderResult.FromError(CommandError.Unsuccessful, string.Format(Texts.InvalidTimeZoneIdError, timeZone));
            return TypeReaderResult.FromSuccess(result);
        }

        private bool DoesDateTimeHasGivenTimezone(DateTime dateTime)
            => dateTime.Kind == DateTimeKind.Local;

        private async Task<(DateTimeOffset?, string)> GetDateTimeWithGuildTimezone(DateTime dateTime, ICustomCommandContext context)
        {
            string timeZone = "UTC";
            if (context.BonusGuild is { })
            {
                var timeZoneSetting = await context.BonusGuild.Settings.Get<string>(typeof(CommonSettings).Assembly, CommonSettings.TimeZone);
                if (timeZoneSetting is { })
                    timeZone = timeZoneSetting;
            }

            if (!TZConvert.TryGetTimeZoneInfo(timeZone, out var timeZoneInfo))
                return (null, timeZone);

            var dateTimeOffset = new DateTimeOffset(dateTime, timeZoneInfo.GetUtcOffset(DateTime.Now));
            return (dateTimeOffset, timeZone);
        }
    }
}
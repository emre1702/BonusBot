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

            var ctx = (ICustomCommandContext)context;
            string timeZone = "UTC";
            if (ctx.BonusGuild is { })
            {
                var timeZoneSetting = await ctx.BonusGuild.Settings.Get<string>(typeof(CommonSettings).Assembly, CommonSettings.TimeZone);
                if (timeZoneSetting is { })
                    timeZone = timeZoneSetting;
            }

            if (!TZConvert.TryGetTimeZoneInfo(timeZone, out var timeZoneInfo))
                return TypeReaderResult.FromError(CommandError.Unsuccessful, string.Format(Texts.InvalidTimeZoneIdError, timeZone));

            var result = new DateTimeOffset(dateTime, timeZoneInfo.BaseUtcOffset);
            return TypeReaderResult.FromSuccess(result);
        }
    }
}
using Discord.Commands;
using BonusBot.Common.Languages;
using System;
using System.Threading.Tasks;
using BonusBot.Common.Extensions;
using System.Threading;
using BonusBot.Common.Defaults;
using BonusBot.Common.Interfaces.Commands;

namespace BonusBot.Common.Commands.TypeReaders
{
    public class TimeSpanTypeReader : TypeReader
    {
        public override Task<TypeReaderResult> ReadAsync(ICommandContext context, string input, IServiceProvider services)
        {
            if (input.TryGetSeconds(out TimeSpan? timeSpan)
                || input.TryGetMinutes(out timeSpan)
                || input.TryGetHours(out timeSpan)
                || input.TryGetDays(out timeSpan)
                || input.TryGetPerma(out timeSpan)
                || input.TryGetZero(out timeSpan))
            {
                return Task.FromResult(TypeReaderResult.FromSuccess(timeSpan));
            }

            Thread.CurrentThread.CurrentUICulture = ((ICustomCommandContext)context).BonusGuild?.Settings.CultureInfo ?? Constants.DefaultCultureInfo;
            return Task.FromResult(TypeReaderResult.FromError(CommandError.ParseFailed, Texts.CommandInvalidTimeSpanError));
        }
    }
}
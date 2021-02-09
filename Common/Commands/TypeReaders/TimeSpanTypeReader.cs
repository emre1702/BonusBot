using Discord.Commands;
using BonusBot.Common.Languages;
using System;
using System.Threading.Tasks;

namespace BonusBot.Common.Commands.TypeReaders
{
    public class TimeSpanTypeReader : TypeReader
    {
        public override Task<TypeReaderResult> ReadAsync(ICommandContext context, string input, IServiceProvider services)
        {
            TimeSpan? timeSpan;
            if (TryGetSeconds(input, out timeSpan)
                || TryGetMinutes(input, out timeSpan)
                || TryGetHours(input, out timeSpan)
                || TryGetDays(input, out timeSpan)
                || TryGetPerma(input, out timeSpan)
                || TryGetZero(input, out timeSpan))
            {
                return Task.FromResult(TypeReaderResult.FromSuccess(timeSpan));
            }

            return Task.FromResult(TypeReaderResult.FromError(CommandError.ParseFailed, Texts.CommandInvalidTimeSpanError));
        }

        private bool TryGetSeconds(string input, out TimeSpan? timeSpan)
        {
            timeSpan = null;
            if (input.EndsWith("s", StringComparison.InvariantCultureIgnoreCase))
            {
                if (!double.TryParse(input[0..^1], out double seconds))
                    return false;
                timeSpan = TimeSpan.FromSeconds(seconds);
                return true;
            }
            if (input.EndsWith("sec", StringComparison.InvariantCultureIgnoreCase))
            {
                if (!double.TryParse(input[0..^3], out double seconds))
                    return false;
                timeSpan = TimeSpan.FromSeconds(seconds);
                return true;
            }
            return false;
        }

        private bool TryGetMinutes(string input, out TimeSpan? timeSpan)
        {
            timeSpan = null;
            if (input.EndsWith("m", StringComparison.InvariantCultureIgnoreCase))
            {
                if (!double.TryParse(input[0..^1], out double minutes))
                    return false;
                timeSpan = TimeSpan.FromMinutes(minutes);
                return true;
            }
            if (input.EndsWith("min", StringComparison.InvariantCultureIgnoreCase))
            {
                if (!double.TryParse(input[0..^3], out double minutes))
                    return false;
                timeSpan = TimeSpan.FromMinutes(minutes);
                return true;
            }
            return false;
        }

        private bool TryGetHours(string input, out TimeSpan? timeSpan)
        {
            timeSpan = null;
            if (input.EndsWith("h", StringComparison.InvariantCultureIgnoreCase))
            {
                if (!double.TryParse(input[0..^1], out double hours))
                    return false;
                timeSpan = TimeSpan.FromHours(hours);
                return true;
            }
            if (input.EndsWith("st", StringComparison.InvariantCultureIgnoreCase))
            {
                if (!double.TryParse(input[0..^2], out double hours))
                    return false;
                timeSpan = TimeSpan.FromHours(hours);
                return true;
            }
            return false;
        }

        private bool TryGetDays(string input, out TimeSpan? timeSpan)
        {
            timeSpan = null;
            if (input.EndsWith("d", StringComparison.InvariantCultureIgnoreCase) || input.EndsWith("t", StringComparison.InvariantCultureIgnoreCase))
            {
                if (!double.TryParse(input[0..^1], out double days))
                    return false;
                timeSpan = TimeSpan.FromDays(days);
                return true;
            }
            return false;
        }

        private bool TryGetPerma(string input, out TimeSpan? timeSpan)
        {
            switch (input.ToLower())
            {
                case "-1":
                case "-":
                case "perma":
                case "permamute":
                case "permaban":
                case "permanent":
                case "never":
                case "nie":
                    timeSpan = TimeSpan.MaxValue;
                    return true;

                default:
                    timeSpan = null;
                    return false;
            }
        }

        private bool TryGetZero(string input, out TimeSpan? timeSpan)
        {
            switch (input.ToLower())
            {
                case "0":
                case "unmute":
                case "unban":
                case "stop":
                case "no":
                case "null":
                    timeSpan = TimeSpan.MinValue;
                    return true;

                default:
                    timeSpan = null;
                    return false;
            }
        }
    }
}
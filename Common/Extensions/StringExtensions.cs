using System;

namespace BonusBot.Common.Extensions
{
    public static class StringExtensions
    {
        public static string ToModuleName(this string str)
            => str
                .Replace("BonusBot.", "")
                .Replace(".dll, ", "")
                .Replace("Module", "");

        public static bool TryGetSeconds(this string input, out TimeSpan? timeSpan)
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

        public static bool TryGetMinutes(this string input, out TimeSpan? timeSpan)
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

        public static bool TryGetHours(this string input, out TimeSpan? timeSpan)
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

        public static bool TryGetDays(this string input, out TimeSpan? timeSpan)
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

        public static bool TryGetPerma(this string input, out TimeSpan? timeSpan)
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

        public static bool TryGetZero(this string input, out TimeSpan? timeSpan)
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

        public static string ReplaceCountrySpecialChars(this string input)
            => input.ReplaceTurkishChars();

        private static string ReplaceTurkishChars(this string input)
            => input
                .Replace('Ğ', 'g')
                .Replace('ğ', 'g')
                .Replace('Ç', 'C')
                .Replace('ç', 'c')
                .Replace('Ş', 'S')
                .Replace('ş', 's');
    }
}
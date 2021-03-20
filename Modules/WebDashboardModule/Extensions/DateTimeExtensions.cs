using System;

namespace BonusBot.WebDashboardModule.Extensions
{
    public static class DateTimeExtensions
    {
        public static long ToUnixTimeSeconds(this DateTime dateTime)
            => new DateTimeOffset(dateTime).ToUnixTimeSeconds();

        public static long ToUnixTimeSeconds(this DateTime dateTime, long afterSeconds)
           => ToUnixTimeSeconds(dateTime.AddSeconds(afterSeconds));
    }
}

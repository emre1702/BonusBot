using DiscordColor = Discord.Color;

namespace BonusBot.WebDashboardModule.Extensions
{
    public static class DiscordColorExtensions
    {
        public static string ToRgbString(this DiscordColor color)
            => $"rgb({color.R}, {color.G}, {color.B})";
    }
}

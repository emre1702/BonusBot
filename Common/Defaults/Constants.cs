using System.Globalization;

namespace BonusBot.Common.Defaults
{
    public static class Constants
    {
        public static CultureInfo Culture => new CultureInfo("de-DE");
        public static string TokenEnvironmentVariable => "BONUSBOT_TOKEN";
        public static string DefaultCommandPrefix => "!";
        public static string DefaultBotName => "BonusBot";
        public static bool DefaultCommandMentionAllowed => true;
    }
}
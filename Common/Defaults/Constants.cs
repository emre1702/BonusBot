using System.Globalization;

namespace BonusBot.Common.Defaults
{
    public static class Constants
    {
        public static CultureInfo DefaultCultureInfo => new CultureInfo("en-US");
        public static string TokenEnvironmentVariable => "BONUSBOT_TOKEN";
        public static string DefaultCommandPrefix => "!";
        public static string DefaultBotName => "BonusBot";
        public static bool DefaultCommandMentionAllowed => true;
    }
}
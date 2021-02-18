using BonusBot.Common.Interfaces;

namespace BonusBot.Common
{
    public class CommonSettings : IGuildSettingsConstantProperties
    {
        public const string BotName = "BotName";
        public const string CommandPrefix = "CommandPrefix";
        public const string CommandPrefixMentionAllowed = "CommandPrefixMentionAllowed";
        public const string Locale = "Locale";
    }
}
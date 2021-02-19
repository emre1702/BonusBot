using BonusBot.Common.Attributes;

namespace BonusBot.Common
{
    [GuildSettingsContainer]
    public class CommonSettings
    {
        public const string BotName = "BotName";
        public const string CommandPrefix = "CommandPrefix";
        public const string CommandPrefixMentionAllowed = "CommandPrefixMentionAllowed";
        public const string Locale = "Locale";
    }
}
using BonusBot.Common.Attributes.Settings;
using BonusBot.Common.Enums;

namespace BonusBot.Common
{
    [GuildSettingsContainer]
    public class CommonSettings
    {
        [GuildSetting(GuildSettingType.String, 1, 100)]
        public const string BotName = "BotName";
        [GuildSetting(GuildSettingType.String, 1, 20, DefaultValue = "!")]
        public const string CommandPrefix = "CommandPrefix";
        [GuildSetting(GuildSettingType.Boolean, DefaultValue = true)]
        public const string CommandPrefixMentionAllowed = "CommandPrefixMentionAllowed";
        [GuildSetting(GuildSettingType.Locale, DefaultValue = "en-US")]
        public const string Locale = "Locale";
        [GuildSetting(GuildSettingType.TimeZone, DefaultValue = "UTC")]
        public const string TimeZone = "TimeZone";
    }
}
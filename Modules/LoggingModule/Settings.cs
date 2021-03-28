using BonusBot.Common.Attributes.Settings;
using BonusBot.Common.Enums;

namespace BonusBot.LoggingModule
{
    [GuildSettingsContainer]
    public class Settings
    {
        [GuildSetting(GuildSettingType.Channel)]
        public const string UserLeftLogChannelId = "UserLeftLogChannelId";
        [GuildSetting(GuildSettingType.Channel)]
        public const string WebCommandExecutedChannel = "WebCommandExecutedChannel";
    }
}
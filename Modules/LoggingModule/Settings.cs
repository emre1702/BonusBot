using BonusBot.Common.Attributes;

namespace BonusBot.LoggingModule
{
    [GuildSettingsContainer]
    public class Settings
    {
        public const string UserLeftLogChannelId = "UserLeftLogChannelId";
        public const string WebCommandExecutedChannel = "WebCommandExecutedChannel";
    }
}
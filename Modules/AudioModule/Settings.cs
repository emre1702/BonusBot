﻿using BonusBot.Common.Attributes.Settings;
using BonusBot.Common.Enums;

namespace BonusBot.AudioModule
{
    [GuildSettingsContainer]
    public class Settings
    {
        [GuildSetting(GuildSettingType.Role)]
        public const string AudioBotUserRoleId = "AudioBotUserRoleId";
        [GuildSetting(GuildSettingType.Channel)]
        public const string AudioInfoChannelId = "AudioInfoChannelId";
        [GuildSetting(GuildSettingType.IntegerSlider, 0, 200, DefaultValue = 100)]
        public const string Volume = "Volume";
    }
}
using BonusBot.Common.Attributes.Settings;
using BonusBot.Common.Enums;

namespace BonusBot.GamePlaningModule
{
    [GuildSettingsContainer]
    public class Settings
    {
        [GuildSetting(GuildSettingType.Emote)]
        public const string ParticipationEmoteId = "ParticipationEmoteId";
        [GuildSetting(GuildSettingType.Emote)]
        public const string LateParticipationEmoteId = "LateParticipationEmoteId";
        [GuildSetting(GuildSettingType.Emote)]
        public const string CancellationEmoteId = "CancellationEmoteId";
        [GuildSetting(GuildSettingType.Emote)]
        public const string MaybeEmoteId = "MaybeEmoteId";
        [GuildSetting(GuildSettingType.Boolean, DefaultValue = false)]
        public const string MentionEveryone = "MentionEveryone";
        [GuildSetting(GuildSettingType.Boolean, DefaultValue = false)]
        public const string RemindAtBeginning = "RemindAtBeginning";
        [GuildSetting(GuildSettingType.Channel)]
        public const string AnnouncementChannel = "AnnouncementChannel";
    }
}
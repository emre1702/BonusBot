using BonusBot.Common.Attributes;

namespace BonusBot.GamePlaningModule
{
    [GuildSettingsContainer]
    public class Settings
    {
        public const string ParticipationEmoteId = "ParticipationEmoteId";
        public const string LateParticipationEmoteId = "LateParticipationEmoteId";
        public const string CancellationEmoteId = "CancellationEmoteId";
        public const string MaybeEmoteId = "MaybeEmoteId";
        public const string MentionEveryone = "MentionEveryone";
        public const string RemindAtBeginning = "RemindAtBeginning";
        public const string AnnouncementChannel = "AnnouncementChannel";
    }
}
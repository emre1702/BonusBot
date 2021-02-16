using BonusBot.Common.Interfaces;

namespace BonusBot.GamePlaningModule
{
    public class Settings : IGuildSettingsConstantProperties
    {
        public const string ParticipationEmoteId = "ParticipationEmoteId";
        public const string LateParticipationEmoteId = "LateParticipationEmoteId";
        public const string CancellationEmoteId = "CancellationEmoteId";
        public const string MaybeEmoteId = "MaybeEmoteId";
        public const string MentionEveryone = "MentionEveryone";
    }
}
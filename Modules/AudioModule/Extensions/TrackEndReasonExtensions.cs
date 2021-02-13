using BonusBot.AudioModule.LavaLink.Enums;

namespace BonusBot.AudioModule.Extensions
{
    internal static class TrackEndReasonExtensions
    {
        public static bool ShouldPlayNext(this TrackEndReason reason)
            => reason == TrackEndReason.Finished || reason == TrackEndReason.LoadFailed;
    }
}
using BonusBot.AudioModule.LavaLink.Clients;
using BonusBot.AudioModule.LavaLink.Enums;

namespace BonusBot.AudioModule.Helpers
{
    public static class WebStateHelper
    {
        public static PlayerStatus? GetPlayerStatus(ulong guildId)
        {
            var player = LavaSocketClient.Instance.GetPlayer(guildId);
            return player?.Status;
        }
    }
}

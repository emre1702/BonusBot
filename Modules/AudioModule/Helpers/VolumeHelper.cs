using BonusBot.AudioModule.LavaLink.Clients;
using BonusBot.Common.Interfaces.Guilds;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.Helpers
{
    public static class VolumeHelper
    {
        public static async ValueTask<int?> GetVolume(IBonusGuild bonusGuild)
        {
            var player = LavaSocketClient.Instance.GetPlayer(bonusGuild.DiscordGuild.Id);
            if (player is not null)
                return player.CurrentVolume;

            int? volume = await bonusGuild.Settings.Get<int>(typeof(Settings).Assembly, Settings.Volume);
            return volume;
        }
    }
}

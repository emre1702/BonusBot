using Discord.WebSocket;
using System.Threading.Tasks;

namespace BonusBot.Common.Interfaces.Guilds
{
    public interface IBonusGuild
    {
        SocketGuild DiscordGuild { get; }
        IGuildSettingsHandler Settings { get; }

        Task Initialize(SocketGuild discordGuild);
    }
}
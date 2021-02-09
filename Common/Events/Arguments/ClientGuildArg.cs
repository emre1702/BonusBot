using Discord.WebSocket;

namespace BonusBot.Common.Events.Arguments
{
    public class ClientGuildArg
    {
        public DiscordSocketClient Client { get; init; }
        public SocketGuild Guild { get; init; }

        public ClientGuildArg(DiscordSocketClient client, SocketGuild guild)
            => (Client, Guild) = (client, guild);
    }
}
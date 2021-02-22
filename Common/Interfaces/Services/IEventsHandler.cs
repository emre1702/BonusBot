using BonusBot.Common.Events;
using BonusBot.Common.Events.Arguments;
using BonusBot.Helper.Events;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace BonusBot.Common.Interfaces.Services
{
    public interface IEventsHandler
    {
        AsyncRelayEvent<ClientGuildArg>? GuildAvailable { get; set; }
        AsyncEvent<MessageData>? Message { get; set; }
        AsyncEvent<MessageData>? MessageCheck { get; set; }

        Task TriggerGuildAvailable(DiscordSocketClient client, SocketGuild guild);

        Task TriggerMessage(SocketMessage socketMessage);
    }
}
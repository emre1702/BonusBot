using Discord.WebSocket;
using BonusBot.Common.Events;
using BonusBot.Common.Events.Arguments;
using BonusBot.Common.Helper;
using BonusBot.Helper.Events;
using System.Threading.Tasks;

namespace BonusBot.Services.Events
{
    public class EventsHandler
    {
        public AsyncRelayEvent<ClientGuildArg>? GuildAvailable;
        public AsyncEvent<MessageData>? Message;
        public AsyncEvent<MessageData>? MessageCheck;

        internal Task TriggerGuildAvailable(DiscordSocketClient client, SocketGuild guild)
            => GuildAvailable?.InvokeAsync(new(client, guild)) ?? Task.CompletedTask;

        internal async Task TriggerMessage(SocketMessage socketMessage)
        {
            MessageData messageData = new(socketMessage);
            await (MessageCheck?.InvokeAsync(messageData) ?? Task.CompletedTask);
            await (Message?.InvokeAsync(messageData) ?? Task.CompletedTask);
        }
    }
}
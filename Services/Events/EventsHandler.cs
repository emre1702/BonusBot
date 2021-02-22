using BonusBot.Common.Events;
using BonusBot.Common.Events.Arguments;
using BonusBot.Helper.Events;
using Discord.WebSocket;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("BonusBot.Core")]

namespace BonusBot.Services.Events
{
    internal class EventsHandler
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
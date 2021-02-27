using BonusBot.Common.Events;
using BonusBot.Common.Events.Arguments;
using BonusBot.Common.Interfaces.Services;
using BonusBot.Helper.Events;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace BonusBot.Services.Events
{
    internal class EventsHandler : IEventsHandler
    {
        public AsyncRelayEvent<ClientGuildArg>? GuildAvailable { get; set; }
        public AsyncEvent<MessageData>? Message { get; set; }
        public AsyncEvent<MessageData>? MessageCheck { get; set; }

        public Task TriggerGuildAvailable(DiscordSocketClient client, SocketGuild guild)
            => GuildAvailable?.InvokeAsync(new(client, guild)) ?? Task.CompletedTask;

        public async Task TriggerMessage(SocketMessage socketMessage)
        {
            MessageData messageData = new(socketMessage);
            await (MessageCheck?.InvokeAsync(messageData) ?? Task.CompletedTask);
            await (Message?.InvokeAsync(messageData) ?? Task.CompletedTask);
        }
    }
}
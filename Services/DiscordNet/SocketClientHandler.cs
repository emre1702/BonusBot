using Discord.WebSocket;
using BonusBot.Database;
using BonusBot.Services.Events;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using BonusBot.Common.Defaults;

namespace BonusBot.Services.DiscordNet
{
    public class SocketClientHandler
    {
        public TaskCompletionSource<DiscordSocketClient> ClientSource { get; } = new TaskCompletionSource<DiscordSocketClient>();

        public SocketClientHandler(EventsHandler eventsHandler)
        {
            InitializeSocketClient(eventsHandler);
        }

        private async void InitializeSocketClient(EventsHandler eventsHandler)
        {
            var client = CreateClient();

            AddEventHandlers(client, eventsHandler);
            await Start(client);

            client.Ready += () => Client_Ready(client);
        }

        private Task Client_Ready(DiscordSocketClient client)
        {
            ClientSource.SetResult(client);
            return Task.CompletedTask;
        }

        private DiscordSocketClient CreateClient()
        {
            var socketClientConfig = new DiscordSocketConfig
            {
                AlwaysDownloadUsers = true,
                ConnectionTimeout = 3 * 60 * 1000,
                DefaultRetryMode = Discord.RetryMode.AlwaysRetry,
                HandlerTimeout = 3 * 60 * 1000,
                ExclusiveBulkDelete = true,
            };
            return new DiscordSocketClient(socketClientConfig);
        }

        private void AddEventHandlers(DiscordSocketClient client, EventsHandler eventsHandler)
        {
            client.GuildAvailable += guild => eventsHandler.TriggerGuildAvailable(client, guild);
            client.MessageReceived += eventsHandler.TriggerMessage;
        }

        private async Task Start(DiscordSocketClient client)
        {
            var token = Environment.GetEnvironmentVariable(Constants.TokenEnvironmentVariable);
            await client.LoginAsync(Discord.TokenType.Bot, token, true);
            await client.StartAsync();
        }
    }
}
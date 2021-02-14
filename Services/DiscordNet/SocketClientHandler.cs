using BonusBot.Common.Defaults;
using BonusBot.Common.Helper;
using BonusBot.Services.Events;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

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
                LogLevel = Discord.LogSeverity.Warning
            };
            return new DiscordSocketClient(socketClientConfig);
        }

        private void AddEventHandlers(DiscordSocketClient client, EventsHandler eventsHandler)
        {
            client.GuildAvailable += guild => eventsHandler.TriggerGuildAvailable(client, guild);
            client.MessageReceived += eventsHandler.TriggerMessage;
            client.Log += message => { ConsoleHelper.Log(message); return Task.CompletedTask; };
        }

        private async Task Start(DiscordSocketClient client)
        {
            var token = Environment.GetEnvironmentVariable(Constants.TokenEnvironmentVariable);
            await client.LoginAsync(Discord.TokenType.Bot, token, true);
            await client.StartAsync();
        }
    }
}
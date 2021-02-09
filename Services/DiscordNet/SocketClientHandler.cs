using Discord.WebSocket;
using BonusBot.Database;
using BonusBot.Services.Events;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BonusBot.Services.DiscordNet
{
    public class SocketClientHandler
    {
        public TaskCompletionSource<DiscordSocketClient> ClientSource { get; } = new TaskCompletionSource<DiscordSocketClient>();

        public SocketClientHandler(FunDbContextFactory dbContextFactory, EventsHandler eventsHandler)
        {
            InitializeSocketClient(dbContextFactory, eventsHandler);
        }

        private async void InitializeSocketClient(FunDbContextFactory dbContextFactory, EventsHandler eventsHandler)
        {
            using var dbContext = dbContextFactory.CreateDbContext();
            var client = CreateClient();

            AddEventHandlers(client, eventsHandler);

            var settings = await dbContext.BotSettings.FirstAsync();

            if (settings.Token.Length == 0)
            {
                var token = await ConnectWithNewToken(client);
                settings.Token = token;
                await dbContext.SaveChangesAsync();
            }
            else
                await ConnectWithToken(client, settings.Token);

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

        private async Task<string> ConnectWithNewToken(DiscordSocketClient client)
        {
            string? token = null;
            while (token is null)
            {
                Console.WriteLine("Kein Bot-Token wurde gespeichert. Bitte gebe den Token des Bots ein.");
                var tryToken = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(tryToken))
                    continue;
                if (!await TryLogin(client, tryToken))
                    continue;
                if (!await TryStart(client))
                    continue;
                token = tryToken;
            }

            return token;
        }

        private async Task<bool> TryLogin(DiscordSocketClient client, string token)
        {
            await client.LoginAsync(Discord.TokenType.Bot, token, true);
            try
            {
                return true;
            }
            catch
            {
                Console.WriteLine("Der Bot konnte sich nicht einloggen.");
                return false;
            }
        }

        private async Task<bool> TryStart(DiscordSocketClient client)
        {
            try
            {
                await client.StartAsync();
                return true;
            }
            catch
            {
                Console.WriteLine("Der Bot konnte nicht gestartet werden.");
                return false;
            }
        }

        private async Task ConnectWithToken(DiscordSocketClient client, string token)
        {
            await client.LoginAsync(Discord.TokenType.Bot, token);
            await client.StartAsync();
        }
    }
}
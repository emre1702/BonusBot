using BonusBot.Common.Interfaces.Guilds;
using BonusBot.LoggingModule.EventHandlers;
using BonusBot.Services.DiscordNet;
using Discord.Commands;

namespace LoggingModule
{
    public class Main : ModuleBase<ICommandContext>
    {
        private static bool _initialized;

        public Main(SocketClientHandler socketClientHandler, IGuildsHandler guildsHandler)
        {
            Initialize(socketClientHandler, guildsHandler);
        }

        private async void Initialize(SocketClientHandler socketClientHandler, IGuildsHandler guildsHandler)
        {
            if (_initialized) return;
            _initialized = true;

            var client = await socketClientHandler.ClientSource.Task;

            client.UserLeft += new UserLeft(guildsHandler).Log;
        }
    }
}
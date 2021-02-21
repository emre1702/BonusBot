using BonusBot.Common.Interfaces.Guilds;
using BonusBot.Common.Interfaces.Services;
using BonusBot.LoggingModule.EventHandlers;
using Discord.Commands;

namespace LoggingModule
{
    public class Main : ModuleBase<ICommandContext>
    {
        private static bool _initialized;

        public Main(IDiscordClientHandler discordClientHandler, IGuildsHandler guildsHandler)
        {
            Initialize(discordClientHandler, guildsHandler);
        }

        private async void Initialize(IDiscordClientHandler discordClientHandler, IGuildsHandler guildsHandler)
        {
            if (_initialized) return;
            _initialized = true;

            var client = await discordClientHandler.ClientSource.Task;

            client.UserLeft += new UserLeft(guildsHandler).Log;
        }
    }
}
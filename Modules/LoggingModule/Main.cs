using BonusBot.Common.Interfaces.Guilds;
using BonusBot.Common.Interfaces.Services;
using BonusBot.LoggingModule.EventHandlers;
using Discord.Commands;
using Discord.Commands.Builders;

namespace LoggingModule
{
    public class Main : ModuleBase<ICommandContext>
    {
        private readonly IDiscordClientHandler _discordClientHandler;
        private readonly IGuildsHandler _guildsHandler;

        public Main(IDiscordClientHandler discordClientHandler, IGuildsHandler guildsHandler)
            => (_discordClientHandler, _guildsHandler) = (discordClientHandler, guildsHandler);

        protected override void OnModuleBuilding(CommandService commandService, ModuleBuilder builder)
        {
            base.OnModuleBuilding(commandService, builder);

            Initialize();
        }

        private async void Initialize()
        {
            var client = await _discordClientHandler.ClientSource.Task;

            client.UserLeft += new UserLeft(_guildsHandler).Log;
        }
    }
}
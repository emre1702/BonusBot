using BonusBot.AudioModule.LavaLink.Clients;
using BonusBot.AudioModule.Models;
using BonusBot.AudioModule.Preconditions;
using BonusBot.Common.Interfaces.Services;
using BonusBot.Database;
using Discord;
using Discord.Commands;
using Discord.Commands.Builders;

namespace BonusBot.AudioModule.PartialMain
{
    [RequireContext(ContextType.Guild)]
    [RequireBotPermission(GuildPermission.Speak & GuildPermission.Connect)]
    [RequireAudioBotRole]
    public partial class Main : AudioCommandBase
    {
        protected static Main? Instance { get; private set; }

        internal static LavaRestClient LavaRestClient = new();
        public BonusDbContextFactory DbContextFactory { get; }

        private readonly IDiscordClientHandler _discordClientHandler;

        public Main(IDiscordClientHandler discordClientHandler, BonusDbContextFactory bonusDbContextFactory)
        {
            Instance = this;
            _discordClientHandler = discordClientHandler;
            DbContextFactory = bonusDbContextFactory;
        }

        protected override async void OnModuleBuilding(CommandService commandService, ModuleBuilder builder)
        {
            base.OnModuleBuilding(commandService, builder);

            var client = await _discordClientHandler.ClientSource.Task;
            await LavaSocketClient.Instance.Start(client);
        }
    }
}
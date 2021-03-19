using BonusBot.Common.Interfaces.Core;
using BonusBot.Common.Interfaces.Guilds;
using BonusBot.Common.Interfaces.Services;
using Discord.Commands;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BonusBot.WebDashboardBoardModule.Services
{
    public class WebCommandService
    {
        private readonly ContextProvideService _contextProvideService;
        private readonly ICommandsHandler _commandsHandler;
        private readonly ICustomServiceProvider _mainServiceProvider;

        public WebCommandService(IGuildsHandler guildsHandler, IDiscordClientHandler discordClientHandler, ICommandsHandler commandsHandler, ICustomServiceProvider mainServiceProvider)
            => (_contextProvideService, _commandsHandler, _mainServiceProvider) = (new(guildsHandler, discordClientHandler), commandsHandler, mainServiceProvider);

        public async Task<IResult> Execute(ISession session, string? guildId, string command)
        {
            var context = await _contextProvideService.Get(session, guildId, command);

            return await _commandsHandler.CommandService.ExecuteAsync(context, 0, _mainServiceProvider, MultiMatchHandling.Best);
        }
    }
}

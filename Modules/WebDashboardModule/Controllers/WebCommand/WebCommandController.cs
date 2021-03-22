using BonusBot.Common.Interfaces.Core;
using BonusBot.Common.Interfaces.Guilds;
using BonusBot.Common.Interfaces.Services;
using BonusBot.WebDashboardModule.Models.WebCommand;
using BonusBot.WebDashboardModule.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BonusBot.WebDashboardModule.Controllers.WebCommand
{
    [Route("[controller]")]
    public class WebCommandController : Controller
    {
        private readonly UserValidationService _userValidationService = new();

        private readonly IGuildsHandler _guildsHandler;
        private readonly IDiscordClientHandler _discordClientHandler;
        private readonly ICommandsHandler _commandsHandler;
        private readonly ICustomServiceProvider _mainServiceProvider;

        public WebCommandController(IGuildsHandler guildsHandler, IDiscordClientHandler discordClientHandler, ICommandsHandler commandsHandler, ICustomServiceProvider mainServiceProvider)
            => (_guildsHandler, _discordClientHandler, _commandsHandler, _mainServiceProvider) = (guildsHandler, discordClientHandler, commandsHandler, mainServiceProvider);

        [HttpPost("Execute")]
        public async Task<IActionResult> Execute([FromBody] WebCommandData commandData)
        {
            if (commandData.GuildId is not null)
                _userValidationService.AssertIsInGuild(HttpContext.Session, commandData.GuildId);
             
            var webCommandService = new WebCommandService(_guildsHandler, _discordClientHandler, _commandsHandler, _mainServiceProvider);
            var messages = await webCommandService.Execute(HttpContext.Session, commandData.GuildId, commandData.Command);

            return Ok(messages);
        }

        [HttpGet("Volume")]
        public async Task<IActionResult> GetVolume(string guildId)
        {
            _userValidationService.AssertIsInGuild(HttpContext.Session, guildId);

            var webCommandService = new WebCommandService(_guildsHandler, _discordClientHandler, _commandsHandler, _mainServiceProvider);
            var messages = await webCommandService.Execute(HttpContext.Session, guildId, "GetVolume true");

            var volumeStr = messages.FirstOrDefault(m => int.TryParse(m, out _));
            if (volumeStr is not null)
                return Ok(new WebVolumeData { Volume = int.Parse(volumeStr) });
            return Ok(new WebVolumeData { Volume = 100 });
        }
    }
}

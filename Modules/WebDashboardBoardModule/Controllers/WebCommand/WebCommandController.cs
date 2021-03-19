using BonusBot.Common.Interfaces.Core;
using BonusBot.Common.Interfaces.Guilds;
using BonusBot.Common.Interfaces.Services;
using BonusBot.WebDashboardBoardModule.Models.WebCommand;
using BonusBot.WebDashboardBoardModule.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BonusBot.WebDashboardBoardModule.Controllers.WebCommand
{
    [Route("[controller]")]
    public class WebCommandController : Controller
    {
        private readonly WebCommandService _webCommandService;
        private readonly UserValidationService _userValidationService = new();

        public WebCommandController(IGuildsHandler guildsHandler, IDiscordClientHandler discordClientHandler, ICommandsHandler commandsHandler, ICustomServiceProvider mainServiceProvider)
            => _webCommandService = new(guildsHandler, discordClientHandler, commandsHandler, mainServiceProvider);

        [HttpPost("Execute")]
        public async Task<IActionResult> Execute([FromBody] WebCommandData commandData)
        {
            if (commandData.GuildId is not null)
                _userValidationService.AssertIsInGuild(HttpContext.Session, commandData.GuildId);
             
            var result = await _webCommandService.Execute(HttpContext.Session, commandData.GuildId, commandData.Command);
            if (!result.IsSuccess)
                return BadRequest(result.Error + Environment.NewLine + result.ErrorReason);
            return Ok();
        }
    }
}

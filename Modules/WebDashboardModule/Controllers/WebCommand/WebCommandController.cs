using BonusBot.Common.Interfaces.Core;
using BonusBot.Common.Interfaces.Guilds;
using BonusBot.Common.Interfaces.Services;
using BonusBot.WebDashboardModule.Models.WebCommand;
using BonusBot.WebDashboardModule.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BonusBot.WebDashboardModule.Controllers.WebCommand
{
    [Route("[controller]")]
    public class WebCommandController : Controller
    {
        private readonly UserValidationService _userValidationService;
        private readonly WebCommandService _webCommandService;

        public WebCommandController(UserValidationService userValidationService, WebCommandService webCommandService)
            => (_userValidationService, _webCommandService) = (userValidationService, webCommandService);

        [HttpPost("Execute")]
        public async Task<IActionResult> Execute([FromBody] WebCommandData commandData)
        {
            if (commandData.GuildId is not null)
                _userValidationService.AssertIsInGuild(HttpContext.Session, commandData.GuildId);

            var (messages, errors) = await _webCommandService.Execute(HttpContext.Session, commandData.GuildId, commandData.Command);

            if (errors.Count > 0)
                return BadRequest(errors);
            return Ok(messages);
        }
    }
}

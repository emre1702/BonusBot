using BonusBot.Common.Interfaces.Guilds;
using BonusBot.Common.Interfaces.Services;
using BonusBot.WebDashboardBoardModule.Enums.Content;
using BonusBot.WebDashboardBoardModule.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BonusBot.WebDashboardBoardModule.Controllers.Content
{
    [Route("[controller]")]
    public class ContentController : Controller
    {
        private readonly UserValidationService _userValidationService;
        private readonly ContentService _contentService;

        public ContentController(IModulesHandler modulesHandler, IGuildsHandler guildsHandler)
            => (_userValidationService, _contentService) = (new(), new(modulesHandler, guildsHandler));

        [HttpGet("UserAccessLevel")]
        public async Task<ActionResult<UserAccessLevel>> GetUserAccessLevel([FromQuery] string module, [FromQuery] string guildId)
        {
            _userValidationService.AssertIsInGuild(HttpContext.Session, guildId);

            var accessLevel = await _contentService.GetUserAccessLevel(module, guildId);
            return Ok(accessLevel);
        }
    }
}

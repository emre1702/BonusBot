using BonusBot.WebDashboardModule.Enums.Content;
using BonusBot.WebDashboardModule.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BonusBot.WebDashboardModule.Controllers.Content
{
    [Route("[controller]")]
    public class ContentController : Controller
    {
        private readonly UserValidationService _userValidationService;
        private readonly ContentService _contentService;

        public ContentController(UserValidationService userValidationService, ContentService contentService)
            => (_userValidationService, _contentService) = (userValidationService, contentService);

        [HttpGet("UserAccessLevel")]
        public async Task<ActionResult<UserAccessLevel>> GetUserAccessLevel([FromQuery] string module, [FromQuery] string guildId)
        {
            _userValidationService.AssertIsInGuild(HttpContext.Session, guildId);

            var accessLevel = await _contentService.GetUserAccessLevel(module, guildId);
            return Ok(accessLevel);
        }
    }
}

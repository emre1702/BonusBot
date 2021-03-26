using BonusBot.Common.Extensions;
using BonusBot.Common.Helper;
using BonusBot.Common.Interfaces.Guilds;
using BonusBot.Common.Interfaces.Services;
using BonusBot.WebDashboardModule.Models.Settings;
using BonusBot.WebDashboardModule.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BonusBot.WebDashboardModule.Controllers.Settings
{
    [Route("[controller]")]
    public class SettingsController : Controller
    {
        private readonly UserValidationService _userValidationService;
        private readonly SettingsService _settingsService;

        public SettingsController(IGuildsHandler guildsHandler, IModulesHandler modulesHandler)
            => (_userValidationService, _settingsService) = (new(), new(guildsHandler, modulesHandler));

        [HttpGet("AllModuleDatas")]
        public ActionResult<IEnumerable<ModuleData>> GetAllModuleDatas([FromQuery] string guildId)
        {
            _userValidationService.AssertIsInGuild(HttpContext.Session, guildId);

            var moduleDatas = _settingsService.GetModuleDatas(guildId);
            return Ok(moduleDatas);
        }

        [HttpGet("ModuleSettings")]
        public async Task<ActionResult<IEnumerable<ModuleSetting>>> GetModuleSettings([FromQuery] string guildId, [FromQuery] string moduleName)
        {
            _userValidationService.AssertIsInGuild(HttpContext.Session, guildId);

            var moduleSettings = await _settingsService.GetModuleSettings(guildId, moduleName);
            return Ok(moduleSettings);
        }
    }
}

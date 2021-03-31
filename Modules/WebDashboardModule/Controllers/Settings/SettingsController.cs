using BonusBot.Common;
using BonusBot.Common.Extensions;
using BonusBot.Common.Models;
using BonusBot.WebDashboardModule.Models.Settings;
using BonusBot.WebDashboardModule.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BonusBot.WebDashboardModule.Controllers.Settings
{
    [Route("[controller]")]
    public class SettingsController : Controller
    {
        private readonly UserValidationService _userValidationService;
        private readonly SettingsService _settingsService;

        public SettingsController(UserValidationService userValidationService, SettingsService settingsService)
            => (_userValidationService, _settingsService) = (userValidationService, settingsService);

        [HttpGet("AllModuleDatas")]
        public ActionResult<IEnumerable<ModuleData>> GetAllModuleDatas([FromQuery] string guildId)
        {
            _userValidationService.AssertIsInGuild(HttpContext.Session, guildId);

            var moduleDatas = _settingsService.GetModuleDatas(guildId);
            return Ok(moduleDatas);
        }

        [HttpGet("ModuleSettings")]
        public async Task<ActionResult<Dictionary<string, GuildSettingData>>> GetModuleSettings([FromQuery] string guildId, [FromQuery] string moduleName)
        {
            _userValidationService.AssertIsInGuild(HttpContext.Session, guildId);

            var moduleSettings = await _settingsService.GetModuleSettings(guildId, moduleName);
            return Ok(moduleSettings);
        }

        [HttpGet("Locale")]
        public async Task<ActionResult<string>> GetGuildLocale([FromQuery] string guildId)
        {
            _userValidationService.AssertIsInGuild(HttpContext.Session, guildId);

            var locale = (await _settingsService.GetModuleSetting(guildId, typeof(CommonSettings).GetModuleName(), CommonSettings.Locale)) ?? "en-US";
            return Ok(locale);
        }
    }
}

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
        private readonly IModulesHandler _modulesHandler;
        private readonly IGuildsHandler _guildsHandler;
        private readonly UserValidationService _userValidationService;

        public SettingsController(IModulesHandler modulesHandler, IGuildsHandler guildsHandler)
            => (_modulesHandler, _guildsHandler, _userValidationService) = (modulesHandler, guildsHandler, new());

        [HttpGet("AllModuleDatas")]
        public ActionResult<IEnumerable<ModuleData>> GetAllModuleDatas([FromQuery] string guildId)
        {
            _userValidationService.AssertIsInGuild(HttpContext.Session, guildId);

            var bonusGuild = _guildsHandler.GetGuild(ulong.Parse(guildId))!;

            var allAssemblyDatas = _modulesHandler.LoadedModuleAssemblies
                .Where(SettingsHelper.HasSettings)
                .Select(a => new ModuleData(a.ToModuleName(), bonusGuild.Modules.Contains(a)))
                .OrderBy(a => a.Name);
            return Ok(allAssemblyDatas);
        }

        [HttpGet("ModuleSettings")]
        public async Task<ActionResult<IEnumerable<ModuleSetting>>> GetModuleSettings([FromQuery] string guildId, [FromQuery] string moduleName)
        {
            _userValidationService.AssertIsInGuild(HttpContext.Session, guildId);

            var bonusGuild = _guildsHandler.GetGuild(ulong.Parse(guildId))!;
            var assembly = bonusGuild.Modules.GetActivatedModuleAssembly(moduleName);
            if (assembly is null) return Ok(Enumerable.Empty<ModuleSetting>());

            var settings = SettingsHelper.GetSettingKeys(assembly).Select(key => new ModuleSetting(key)).ToList();
            foreach (var setting in settings)
                setting.Value = (await bonusGuild.Settings.Get<object?>(assembly, setting.Name))?.GetIdentifier() ?? string.Empty;

            return Ok(settings);
        }
    }
}

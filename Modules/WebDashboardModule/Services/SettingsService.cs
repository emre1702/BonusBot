using BonusBot.Common;
using BonusBot.Common.Extensions;
using BonusBot.Common.Helper;
using BonusBot.Common.Interfaces.Guilds;
using BonusBot.Common.Interfaces.Services;
using BonusBot.WebDashboardModule.Models.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BonusBot.WebDashboardModule.Services
{
    public class SettingsService
    {
        private readonly IGuildsHandler _guildsHandler;
        private readonly IModulesHandler _modulesHandler;

        public SettingsService(IGuildsHandler guildsHandler, IModulesHandler modulesHandler)
            => (_guildsHandler, _modulesHandler) = (guildsHandler, modulesHandler);

        public IEnumerable<ModuleData> GetModuleDatas(string guildId)
        {
            var bonusGuild = _guildsHandler.GetGuild(ulong.Parse(guildId))!;

            var allAssemblyDatas = _modulesHandler.LoadedModuleAssemblies
                .Where(SettingsHelper.HasSettings)
                .Select(a => new ModuleData(a.ToModuleName(), bonusGuild.Modules.Contains(a)))
                .Union(new List<ModuleData> { new(typeof(CommonSettings).GetModuleName(), true) })
                .OrderBy(a => a.Name);

            return allAssemblyDatas;
        }

        public async Task<IEnumerable<ModuleSetting>> GetModuleSettings(string guildId, string moduleName)
        {
            var bonusGuild = _guildsHandler.GetGuild(ulong.Parse(guildId))!;
            var assembly = moduleName != typeof(CommonSettings).GetModuleName() ? bonusGuild.Modules.GetActivatedModuleAssembly(moduleName) : typeof(CommonSettings).Assembly; 
            if (assembly is null) return Enumerable.Empty<ModuleSetting>();

            var settings = SettingsHelper.GetSettingKeys(assembly).Select(key => new ModuleSetting(key)).ToList();
            foreach (var setting in settings)
                setting.Value = (await bonusGuild.Settings.Get<object?>(assembly, setting.Name))?.GetIdentifier() ?? string.Empty;

            return settings;
        }
    }
}

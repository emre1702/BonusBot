using BonusBot.Common;
using BonusBot.Common.Extensions;
using BonusBot.Common.Helper;
using BonusBot.Common.Interfaces.Guilds;
using BonusBot.Common.Interfaces.Services;
using BonusBot.Common.Models;
using BonusBot.WebDashboardModule.Models.Settings;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BonusBot.WebDashboardModule.Services
{
    public class SettingsService
    {
        private readonly IGuildsHandler _guildsHandler;
        private readonly IModulesHandler _modulesHandler;
        private readonly ISettingsHandler _settingsHandler;

        public SettingsService(IGuildsHandler guildsHandler, IModulesHandler modulesHandler, ISettingsHandler settingsHandler)
            => (_guildsHandler, _modulesHandler, _settingsHandler) = (guildsHandler, modulesHandler, settingsHandler);

        public IEnumerable<ModuleData> GetModuleDatas(string guildId)
        {
            var bonusGuild = _guildsHandler.GetGuild(ulong.Parse(guildId))!;

            var allAssemblyDatas = _modulesHandler.LoadedModuleAssemblies
                .Select(a => new ModuleData(a.ToModuleName(), bonusGuild.Modules.Contains(a), CanBeDisabled(a)))
                .Union(new List<ModuleData> { new(typeof(CommonSettings).GetModuleName(), true, false) })
                .OrderBy(a => a.Name);

            return allAssemblyDatas;
        }

        private bool CanBeDisabled(Assembly assembly) 
            => assembly.ToModuleName() switch
            {
                "ModulesControlling" or "Common" => false,
                _ => true 
            };

        public async Task<Dictionary<string, GuildSettingData>> GetModuleSettings(string guildId, string moduleName)
        {
            var bonusGuild = _guildsHandler.GetGuild(ulong.Parse(guildId))!;
            var assembly = moduleName != typeof(CommonSettings).GetModuleName() ? bonusGuild.Modules.GetActivatedModuleAssembly(moduleName) : typeof(CommonSettings).Assembly; 
            if (assembly is null) return new();

            var settings = _settingsHandler.GetDatas(assembly.ToModuleName());
            foreach (var settingEntry in settings)
                settingEntry.Value.Value = (await bonusGuild.Settings.Get<object?>(assembly, settingEntry.Key))?.GetIdentifier() ?? string.Empty;

            return settings;
        }
    }
}

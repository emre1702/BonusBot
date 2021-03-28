using BonusBot.Common;
using BonusBot.Common.Attributes.Settings;
using BonusBot.Common.Enums;
using BonusBot.Common.Extensions;
using BonusBot.Common.Helper;
using BonusBot.Common.Interfaces.Services;
using BonusBot.Common.Models;
using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BonusBot.Services.Settings
{
    public class SettingsHandler : ISettingsHandler
    {
        private readonly Dictionary<string, Dictionary<string, GuildSettingData>> _moduleSettings = new();

        public SettingsHandler(IModulesHandler modulesHandler)
        {
            LoadAllSettings(modulesHandler);
        }

        public GuildSettingData GetData(string moduleName, string key)
        {
            lock (_moduleSettings)
            {
                return _moduleSettings[moduleName][key];
            }
        }

        public Dictionary<string, GuildSettingData> GetDatas(string moduleName)
        {
            lock (_moduleSettings)
            {
                return new(_moduleSettings[moduleName]);
            }
        }

        private async void LoadAllSettings(IModulesHandler modulesHandler)
        {
            try
            {
                await modulesHandler.LoadModulesTaskSource.Task;

                lock (_moduleSettings)
                {
                    foreach (var (module, name) in GetModules(modulesHandler))
                        _moduleSettings[name] = GetDatas(module, name).ToDictionary(e => e.Item1, e => e.Item2);
                }
            }
            catch (Exception ex)
            {
                ConsoleHelper.Log(LogSeverity.Critical, LogSource.Core, "Error occured when loading settings.", ex);
            }
        }

        private IEnumerable<(Assembly, string)> GetModules(IModulesHandler modulesHandler)
            => modulesHandler.LoadedModuleAssemblies.Union(new List<Assembly> { typeof(CommonSettings).Assembly }).Select(a => (a, a.ToModuleName()));

        private IEnumerable<(string, GuildSettingData)> GetDatas(Assembly assembly, string moduleName)
        {
            var fields = SettingsHelper.GetSettingFields(assembly);
            foreach (var field in fields)
            {
                var settingAttribute = field.GetCustomAttribute<GuildSettingAttribute>();
                if (settingAttribute is null)
                    settingAttribute = new GuildSettingAttribute(GuildSettingType.String);
                yield return (field.GetRawConstantValue()!.ToString()!, new(moduleName, field.Name, settingAttribute));
            }
        }
    }
}

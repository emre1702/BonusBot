using BonusBot.Common.Attributes;
using BonusBot.Common.Extensions;
using BonusBot.Common.Interfaces;
using BonusBot.Services.DiscordNet;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GuildSettingsModule
{
    internal static class Helpers
    {
        internal static bool DoesSettingExists(ModulesHandler modulesHandler, string key, string moduleName)
        {
            var assembly = modulesHandler.FindAssemblyByModuleName(moduleName);
            if (assembly is null)
                return false;

            var settingsKeys = GetSettingKeys(assembly);

            return settingsKeys.Any(k => k.Equals(key, System.StringComparison.CurrentCultureIgnoreCase));
        }

        private static IEnumerable<FieldInfo> GetSettingFields(Assembly assembly)
        {
            var classesWithSettings = assembly.GetTypes().Where(t => t.GetCustomAttribute<GuildSettingsContainerAttribute>() is { });
            var fields = classesWithSettings.SelectMany(t => t.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy).Where(fi => fi.IsLiteral && !fi.IsInitOnly));
            return fields;
        }

        private static IEnumerable<string> GetSettingKeys(Assembly assembly)
        {
            var fields = GetSettingFields(assembly);
            return fields.Where(fi => fi.GetRawConstantValue() != null).Select(fi => fi.GetRawConstantValue()!.ToString()!);
        }

        internal static string GetAllModulesNamesJoined(ModulesHandler modulesHandler)
        {
            var modulesStr = string.Empty;
            lock (modulesHandler.LoadedModuleAssemblies)
            {
                var moduleNames = modulesHandler.LoadedModuleAssemblies.Select(a => a.GetName()?.Name?.ToString().ToModuleName());
                modulesStr = string.Join(", ", moduleNames);
            }
            return modulesStr;
        }

        internal static string GetAllModulesAndCommonNamesJoined(ModulesHandler modulesHandler)
            => "Common, " + GetAllModulesNamesJoined(modulesHandler);

        internal static string GetModuleSettingsJoined(ModulesHandler modulesHandler, string moduleName)
        {
            var module = modulesHandler.FindAssemblyByModuleName(moduleName);
            if (module is null)
                return "-";
            var settingKeys = GetSettingKeys(module);
            return string.Join(", ", settingKeys);
        }
    }
}
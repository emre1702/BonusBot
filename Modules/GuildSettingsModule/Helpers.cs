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

            var settingsType = typeof(IGuildSettingsConstantProperties);
            var settingsKeys = GetSettingKeys(assembly);

            return settingsKeys.Any(k => k.Equals(key, System.StringComparison.CurrentCultureIgnoreCase));
        }

        private static IEnumerable<FieldInfo> GetSettingFields(Assembly assembly)
        {
            var settingsType = typeof(IGuildSettingsConstantProperties);
            var classesWithSettings = assembly.GetTypes().OfType<TypeInfo>().Where(t => t.ImplementedInterfaces.Any(i => i == settingsType));
            var fields = classesWithSettings.SelectMany(t => t.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy).Where(fi => fi.IsLiteral && !fi.IsInitOnly));
            return fields;
        }

        private static IEnumerable<string> GetSettingKeys(Assembly assembly)
        {
            var fields = GetSettingFields(assembly);
            return fields.Where(fi => fi.GetRawConstantValue() != null).Select(fi => fi.GetRawConstantValue()!.ToString()!);
        }
    }
}
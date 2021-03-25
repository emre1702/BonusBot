using BonusBot.Common.Attributes;
using BonusBot.Common.Interfaces.Guilds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BonusBot.Common.Helper
{
    public class SettingsHelper
    {
        public static bool DoesSettingExists(IBonusGuild bonusGuild, string key, string moduleName)
        {
            var assembly = bonusGuild.Modules.GetActivatedModuleAssembly(moduleName);
            if (assembly is null)
                return false;

            var settingsKeys = GetSettingKeys(assembly);

            return settingsKeys.Any(k => k.Equals(key, StringComparison.CurrentCultureIgnoreCase));
        }

        public static bool HasSettings(Assembly assembly)
            => assembly.GetTypes().Any(t => t.GetCustomAttribute<GuildSettingsContainerAttribute>() is { });

        public static IEnumerable<Type> GetClassesWithSettings(Assembly assembly)
            => assembly.GetTypes().Where(t => t.GetCustomAttribute<GuildSettingsContainerAttribute>() is { });

        public static IEnumerable<FieldInfo> GetSettingFields(Assembly assembly)
        {
            var classesWithSettings = GetClassesWithSettings(assembly);
            var fields = classesWithSettings.SelectMany(t => t.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy).Where(fi => fi.IsLiteral && !fi.IsInitOnly));
            return fields;
        }

        public static IEnumerable<string> GetSettingKeys(Assembly assembly)
        {
            var fields = GetSettingFields(assembly);
            return fields.Where(fi => fi.GetRawConstantValue() != null).Select(fi => fi.GetRawConstantValue()!.ToString()!);
        }

        public static string GetModuleSettingsJoined(IBonusGuild bonusGuild, string moduleName)
        {
            var module = bonusGuild.Modules.GetActivatedModuleAssembly(moduleName);
            if (module is null)
                return "-";
            var settingKeys = GetSettingKeys(module);
            return string.Join(", ", settingKeys);
        }
    }
}
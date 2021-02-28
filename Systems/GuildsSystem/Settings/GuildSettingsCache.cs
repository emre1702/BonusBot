using BonusBot.Common.Extensions;
using BonusBot.Common.Interfaces.Guilds;
using System.Collections.Generic;
using System.Reflection;

namespace BonusBot.GuildsSystem.Settings
{
    internal class GuildSettingsCache : IGuildSettingsCache
    {
        private readonly Dictionary<string, Dictionary<string, object>> _keyValueSettingByModuleName = new();

        public object? Get(string moduleName, string key)
        {
            lock (_keyValueSettingByModuleName)
            {
                if (!_keyValueSettingByModuleName.ContainsKey(moduleName))
                    _keyValueSettingByModuleName[moduleName] = new();
                _keyValueSettingByModuleName[moduleName].TryGetValue(key, out var value);
                return value;
            }
        }

        public object? Get(Assembly assembly, string key)
            => Get(assembly.ToModuleName(), key);

        public void Set(string moduleName, string key, object value)
        {
            lock (_keyValueSettingByModuleName)
            {
                if (!_keyValueSettingByModuleName.ContainsKey(moduleName))
                    _keyValueSettingByModuleName[moduleName] = new();
                _keyValueSettingByModuleName[moduleName][key] = value;
            }
        }

        public void Set(Assembly assembly, string key, object value)
            => Set(assembly.ToModuleName(), key, value);

        public void Remove(string moduleName, string key)
        {
            lock (_keyValueSettingByModuleName)
            {
                if (!_keyValueSettingByModuleName.ContainsKey(moduleName))
                    return;
                _keyValueSettingByModuleName[moduleName].Remove(key);
            }
        }

        public void Remove(Assembly assembly, string key)
            => Remove(assembly.ToModuleName(), key);
    }
}
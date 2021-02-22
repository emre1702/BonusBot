using System.Reflection;

namespace BonusBot.Common.Interfaces.Guilds
{
    public interface IGuildSettingsCache
    {
        object? Get(string moduleName, string key);

        object? Get(Assembly assembly, string key);

        void Set(string moduleName, string key, object value);

        void Set(Assembly assembly, string key, object value);

        void Remove(string moduleName, string key);

        void Remove(Assembly assembly, string key);
    }
}
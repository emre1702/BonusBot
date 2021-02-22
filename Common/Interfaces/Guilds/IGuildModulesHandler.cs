using System.Reflection;

namespace BonusBot.Common.Interfaces.Guilds
{
    public interface IGuildModulesHandler
    {
        bool Add(Assembly assembly);

        bool Contains(Assembly assembly);

        bool Remove(Assembly assembly);
    }
}
using BonusBot.Common.Interfaces.Guilds;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("BonusBot.Core")]

namespace BonusBot.GuildsSystem.Modules
{
    internal class GuildModulesHandler : IGuildModulesHandler
    {
        private readonly HashSet<Assembly> _moduleAssemblies = new();

        public bool Add(Assembly assembly)
        {
            lock (_moduleAssemblies)
            {
                return _moduleAssemblies.Add(assembly);
            }
        }

        public bool Remove(Assembly assembly)
        {
            lock (_moduleAssemblies)
            {
                return _moduleAssemblies.Remove(assembly);
            }
        }

        public bool Contains(Assembly assembly)
        {
            lock (_moduleAssemblies)
            {
                return _moduleAssemblies.Contains(assembly);
            }
        }
    }
}
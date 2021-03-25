using Discord.Commands;
using Discord.WebSocket;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace BonusBot.Common.Interfaces.Guilds
{
    public interface IGuildModulesHandler
    {
        ValueTask<bool> Add(Assembly assembly);

        bool Contains(Assembly assembly);
        bool Contains(ModuleInfo moduleInfo);

        List<Assembly> GetActivatedModuleAssemblies();

        Assembly? GetActivatedModuleAssembly(string moduleName);

        Task Init(IGuildSettingsHandler settingsHandler, SocketGuild discordGuild);

        ValueTask<bool> Remove(Assembly assembly);

        string ToString();
    }
}
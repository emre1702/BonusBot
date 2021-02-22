using Discord.WebSocket;
using System.Reflection;
using System.Threading.Tasks;

namespace BonusBot.Common.Interfaces.Guilds
{
    public interface IGuildModulesHandler
    {
        ValueTask<bool> Add(Assembly assembly);

        bool Contains(Assembly assembly);

        ValueTask<bool> Remove(Assembly assembly);

        Task Init(IGuildSettingsHandler settingsHandler, SocketGuild discordGuild);

        Assembly? GetActivatedModuleAssembly(string moduleName);
    }
}
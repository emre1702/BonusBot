using Discord.WebSocket;
using System.Reflection;
using System.Threading.Tasks;

namespace BonusBot.Common.Interfaces.Guilds
{
    public interface IGuildSettingsHandler
    {
        void Init(SocketGuild guild);

        ValueTask<T?> Get<T>(string moduleName, string key);

        ValueTask<T?> Get<T>(Assembly moduleAssembly, string key);

        Task Set(string moduleName, string key, object value);
    }
}
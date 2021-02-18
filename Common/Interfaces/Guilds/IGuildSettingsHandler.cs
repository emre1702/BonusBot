using Discord.WebSocket;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;

namespace BonusBot.Common.Interfaces.Guilds
{
    public interface IGuildSettingsHandler
    {
        CultureInfo CultureInfo { get; set; }

        Task Init(SocketGuild guild);

        ValueTask<T?> Get<T>(string moduleName, string key);

        ValueTask<T?> Get<T>(Assembly moduleAssembly, string key);

        Task Set(string moduleName, string key, object value);

        Task Set(Assembly moduleAssembly, string key, object value);
    }
}
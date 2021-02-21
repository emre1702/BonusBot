using Discord.WebSocket;
using System.Threading.Tasks;

namespace BonusBot.Common.Interfaces.Services
{
    public interface IDiscordClientHandler
    {
        TaskCompletionSource<DiscordSocketClient> ClientSource { get; }
    }
}
using BonusBot.Common.Interfaces.Guilds;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace BonusBot.Common.Interfaces.Commands
{
    public interface ICustomCommandContext : ICommandContext
    {
        IBonusGuild? BonusGuild { get; }
        SocketGuildUser? GuildUser { get; }
        bool IsWeb => false;

        ValueTask<IUser?> GetUserAsync(ulong id);
    }
}

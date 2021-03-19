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

        ValueTask<IUser?> GetUserAsync(ulong id);
    }
}

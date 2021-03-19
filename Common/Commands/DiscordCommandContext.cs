using BonusBot.Common.Events.Arguments;
using BonusBot.Common.Extensions;
using BonusBot.Common.Interfaces.Commands;
using BonusBot.Common.Interfaces.Guilds;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace BonusBot.Common.Commands
{
    public class DiscordCommandContext : SocketCommandContext, ICustomCommandContext
    {
        public IBonusGuild? BonusGuild { get; }
        public MessageData MessageData { get; }
        public SocketGuildUser? GuildUser { get; }
        public SocketUser SocketUser { get; }

        public DiscordCommandContext(DiscordSocketClient client, MessageData msgData, IGuildsHandler guildsHandler) : base(client, msgData.Message as SocketUserMessage)
        {
            GuildUser = msgData.Message.Author.CastTo<SocketGuildUser>();
            SocketUser = msgData.Message.Author;
            MessageData = msgData;
            if (Guild is { })
                BonusGuild = guildsHandler.GetGuild(Guild)!;
        }

        public async ValueTask<IUser?> GetUserAsync(ulong id)
            => Guild.GetUser(id).CastTo<IUser>() ?? (await Client.Rest.GetUserAsync(id)).CastTo<IUser>();
    }
}
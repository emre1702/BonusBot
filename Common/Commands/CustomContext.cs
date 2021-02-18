using BonusBot.Common.Events.Arguments;
using BonusBot.Common.Extensions;
using BonusBot.Common.Interfaces.Guilds;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace BonusBot.Common.Commands
{
    public class CustomContext : SocketCommandContext
    {
        public IBonusGuild BonusGuild { get; }
        public MessageData MessageData { get; }
        public new SocketGuildUser? User { get; }
        public SocketUser SocketUser { get; }

        public CustomContext(DiscordSocketClient client, MessageData msgData, IGuildsHandler guildsHandler) : base(client, msgData.Message as SocketUserMessage)
        {
            User = msgData.Message.Author.CastTo<SocketGuildUser>();
            SocketUser = msgData.Message.Author;
            MessageData = msgData;
            BonusGuild = guildsHandler.GetGuild(Guild)!;
            //SocketUser = base.User;
        }

        public async ValueTask<IUser?> GetUserAsync(ulong id)
            => Guild.GetUser(id).CastTo<IUser>() ?? (await Client.Rest.GetUserAsync(id)).CastTo<IUser>();
    }
}
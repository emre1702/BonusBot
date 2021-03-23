using BonusBot.Common.Interfaces.Commands;
using BonusBot.Common.Interfaces.Guilds;
using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace BonusBot.WebDashboardModule.Discord
{
    public class WebCommandContext : ICustomCommandContext
    {
        public Guid Id { get; }
        public IBonusGuild? BonusGuild { get; }

        public IDiscordClient Client { get; }

        public IGuild? Guild { get; }

        public SocketGuildUser? GuildUser { get; }

        public IMessageChannel Channel { get; }

        public IUser User { get; }

        public IUserMessage Message { get; }

        public bool IsWeb => true;

        public WebCommandContext(Guid id, IBonusGuild? bonusGuild, DiscordSocketClient client, SocketGuild? guild, IMessageChannel channel, IUser user, SocketGuildUser? socketGuildUser, IUserMessage message)
        {
            Id = id;
            BonusGuild = bonusGuild;
            Client = client;
            Guild = guild;
            GuildUser = socketGuildUser;
            Channel = channel;
            User = user;
            Message = message;
        }

        public async ValueTask<IUser?> GetUserAsync(ulong id)
        {
            IUser? user = null;
            if (Guild is not null)
                user = await Guild.GetUserAsync(id);
            if (user is null && Client is DiscordSocketClient discordSocketClient)
                user = await discordSocketClient.Rest.GetUserAsync(id);

            return user;
        }
    }
}

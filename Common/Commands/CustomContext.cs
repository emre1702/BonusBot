using Discord;
using Discord.Commands;
using Discord.WebSocket;
using BonusBot.Common.Events.Arguments;
using BonusBot.Common.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BonusBot.Common.Commands
{
    public class CustomContext : SocketCommandContext
    {
        public MessageData MessageData { get; }
        public new SocketGuildUser? User { get; }
        public SocketUser SocketUser { get; }
        public Dictionary<string, object> RequiredSettingValues { get; } = new Dictionary<string, object>();

        public CustomContext(DiscordSocketClient client, MessageData msgData) : base(client, msgData.Message as SocketUserMessage)
        {
            User = msgData.Message.Author.CastTo<SocketGuildUser>();
            SocketUser = msgData.Message.Author;
            MessageData = msgData;
            //SocketUser = base.User;
        }

        public async ValueTask<IUser?> GetUserAsync(ulong id)
            => Guild.GetUser(id).CastTo<IUser>() ?? (await Client.Rest.GetUserAsync(id)).CastTo<IUser>();

        public T GetRequiredSettingValue<T>(string key)
            => (T)RequiredSettingValues[key];

        public GuildEmote GetRequiredEmoteSetting(string key)
            => (GuildEmote)RequiredSettingValues[key];

        public SocketTextChannel GetRequiredChannelSetting(string key)
            => (SocketTextChannel)RequiredSettingValues[key];
    }
}
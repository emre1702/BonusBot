using BonusBot.Common.Interfaces.Guilds;
using BonusBot.LoggingModule.Language;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace BonusBot.LoggingModule.EventHandlers
{
    internal class UserLeft
    {
        private readonly IGuildsHandler _guildsHandler;

        public UserLeft(IGuildsHandler guildsHandler)
            => _guildsHandler = guildsHandler;

        internal async Task Log(SocketGuildUser arg)
        {
            var bonusGuild = _guildsHandler.GetGuild(arg.Guild.Id);
            if (bonusGuild is null) return;

            var channel = await bonusGuild.Settings.Get<SocketTextChannel>(GetType().Assembly, Settings.UserLeftLogChannelId);
            if (channel is null) return;

            var userName = arg.Username + "#" + arg.Discriminator;
            if (!string.IsNullOrWhiteSpace(arg.Nickname))
                userName = arg.Nickname + " / " + userName;
            await channel.SendMessageAsync(string.Format(ModuleTexts.PlayerLeftGuildLogInfo, arg.Mention, userName));
        }
    }
}
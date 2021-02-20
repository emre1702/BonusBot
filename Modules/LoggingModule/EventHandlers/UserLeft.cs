using BonusBot.Common.Extensions;
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

        internal async Task Log(SocketGuildUser user)
        {
            var bonusGuild = _guildsHandler.GetGuild(user.Guild.Id);
            if (bonusGuild is null) return;

            var channel = await bonusGuild.Settings.Get<SocketTextChannel>(GetType().Assembly, Settings.UserLeftLogChannelId);
            if (channel is null) return;

            await channel.SendMessageAsync(string.Format(ModuleTexts.PlayerLeftGuildLogInfo, user.Mention, user.GetFullNameInfo()));
        }
    }
}
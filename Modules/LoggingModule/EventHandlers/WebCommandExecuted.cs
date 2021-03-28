using BonusBot.Common.Extensions;
using BonusBot.Common.Interfaces.Commands;
using BonusBot.Common.Interfaces.Guilds;
using BonusBot.LoggingModule.Language;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace BonusBot.LoggingModule.EventHandlers
{
    internal class WebCommandExecuted
    {
        private readonly IGuildsHandler _guildsHandler;

        public WebCommandExecuted(IGuildsHandler guildsHandler)
            => _guildsHandler = guildsHandler;

        internal async Task Log(Optional<CommandInfo> _1, ICommandContext context, IResult _2)
        {
            if (context is not ICustomCommandContext { IsWeb: true } webContext)
                return;
            if (IsCommandToIgnore(webContext.Message.Content))
                return;

            var bonusGuild = _guildsHandler.GetGuild(webContext.Guild.Id);
            if (bonusGuild is null) return;

            var channel = await bonusGuild.Settings.Get<SocketTextChannel>(GetType().Assembly, Settings.WebCommandExecutedChannel);
            if (channel is null) return;

            await channel.SendMessageAsync(string.Format(ModuleTexts.WebCommandExecutedInfo, GetUserName(webContext), context.Message.Content));
        }

        private string GetUserName(ICustomCommandContext context)
        {
            if (context.GuildUser is { } guildUser)
                return guildUser.GetFullNameInfo();
            return context.User.Username + "#" + context.User.Discriminator;
        }

        private bool IsCommandToIgnore(string command)
            => command switch
            {
                string s when s.StartsWith("GetState", System.StringComparison.OrdinalIgnoreCase) => true,
                _ => false
            };
    }
}

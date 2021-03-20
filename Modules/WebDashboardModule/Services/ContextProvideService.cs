using BonusBot.Common.Interfaces.Guilds;
using BonusBot.Common.Interfaces.Services;
using BonusBot.WebDashboardModule.Defaults;
using BonusBot.WebDashboardModule.Discord;
using BonusBot.WebDashboardModule.Extensions;
using BonusBot.WebDashboardModule.Models.Discord;
using Discord.WebSocket;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BonusBot.WebDashboardModule.Services
{
    public class ContextProvideService
    {
        private readonly IGuildsHandler _guildsHandler;
        private readonly IDiscordClientHandler _discordClientHandler;

        public ContextProvideService(IGuildsHandler guildsHandler, IDiscordClientHandler discordClientHandler)
            => (_guildsHandler, _discordClientHandler) = (guildsHandler, discordClientHandler);


        public async Task<WebCommandContext> Get(ISession session, string? guildId, string command)
        {
            var discordClient = await _discordClientHandler.ClientSource.Task;

            var guildIdUlong = guildId is not null ? ulong.Parse(guildId) : (ulong?)null;
            var bonusGuild = _guildsHandler.GetGuild(guildIdUlong);
            var guild = guildIdUlong.HasValue ? discordClient.GetGuild(guildIdUlong.Value) : null;
            var socketGuildUser = guild is not null ? GetGuildUser(session, guild) : null;
            var user = new WebUser();
            var channel = new WebMessageChannel(user);
            var message = new WebMessage(command, user, channel);

            return new(bonusGuild, discordClient, guild, channel, user, socketGuildUser, message);
        }

        private static SocketGuildUser? GetGuildUser(ISession session, SocketGuild guild)
        {
            var userData = session.Get<UserResponseData>(SessionKeys.UserData);
            return guild.GetUser(userData!.Id);
        }
    }
}

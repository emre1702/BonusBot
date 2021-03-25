using BonusBot.Common.Interfaces.Guilds;
using BonusBot.Common.Interfaces.Services;
using BonusBot.WebDashboardModule.Defaults;
using BonusBot.WebDashboardModule.Discord;
using BonusBot.WebDashboardModule.Extensions;
using BonusBot.WebDashboardModule.Models.Discord;
using Discord;
using Discord.WebSocket;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BonusBot.WebDashboardModule.Services
{
    public class ContextProvideService
    {
        private readonly IGuildsHandler _guildsHandler;
        private readonly IDiscordClientHandler _discordClientHandler;

        public ContextProvideService(IGuildsHandler guildsHandler, IDiscordClientHandler discordClientHandler)
            => (_guildsHandler, _discordClientHandler) = (guildsHandler, discordClientHandler);


        public async Task<WebCommandContext> Get(ISession session, string? guildId, Guid guid, string command)
        {
            var discordClient = await _discordClientHandler.ClientSource.Task;
            var userData = session.Get<UserResponseData>(SessionKeys.UserData)!;

            var guildIdUlong = guildId is not null ? ulong.Parse(guildId) : (ulong?)null;
            var bonusGuild = _guildsHandler.GetGuild(guildIdUlong);
            var guild = guildIdUlong.HasValue ? discordClient.GetGuild(guildIdUlong.Value) : null;
            var guildUser = guild is not null ? GetGuildUser(userData, guild) : null;
            var user = guildUser is not null ? new WebGuildUser(userData, guildUser) : new WebUser(userData);
            IMessageChannel channel = guild is null ? new WebDMChannel(user) : new WebGuildMessageChannel(user, guild);
            var message = new WebMessage(command, user, channel);

            return new(guid, bonusGuild, discordClient, guild, channel, user, guildUser, message);
        }

        private static SocketGuildUser? GetGuildUser(UserResponseData userData, SocketGuild guild) 
            => guild.GetUser(userData!.Id);
    }
}

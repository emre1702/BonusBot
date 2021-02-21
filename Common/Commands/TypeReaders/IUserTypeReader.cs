﻿using BonusBot.Common.Languages;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BonusBot.Common.Commands.TypeReaders
{
    public class IUserTypeReader : TypeReader
    {
        public override async Task<TypeReaderResult> ReadAsync(ICommandContext context, string input, IServiceProvider services)
        {
            var ctx = (CustomContext)context;
            IUser? user = null;
            if (MentionUtils.TryParseUser(input, out ulong userId)
                || ulong.TryParse(input, out userId))
                user ??= await ctx.GetUserAsync(userId).ConfigureAwait(false);

            user ??= await GetSocketGuildUserByName(ctx.Guild, input);
            user ??= await GetUserByBan(ctx.Guild, input);

            return user is { }
                ? TypeReaderResult.FromSuccess(user)
                : TypeReaderResult.FromError(CommandError.ParseFailed, Texts.CommandInvalidBooleanError);
        }

        private async Task<IUser?> GetSocketGuildUserByName(SocketGuild guild, string input)
        {
            var users = (await guild.GetUsersAsync().FlattenAsync().ConfigureAwait(false)).ToList();
            return GetUser(users, input);
        }

        private async Task<IUser?> GetUserByBan(SocketGuild guild, string input)
        {
            var bans = await guild.GetBansAsync();
            return GetUser(bans.Select(b => b.User), input);
        }

        private IUser? GetUser(IEnumerable<IUser> users, string name)
        {
            IUser? user = users.OfType<SocketGuildUser>().FirstOrDefault(u => u.Nickname.Equals(name, StringComparison.CurrentCultureIgnoreCase));
            user ??= users.FirstOrDefault(u => u.Username.Equals(name, StringComparison.CurrentCultureIgnoreCase));
            user ??= users.FirstOrDefault(u => (u.Username + "#" + u.Discriminator).Equals(name, StringComparison.CurrentCultureIgnoreCase));
            user ??= users.FirstOrDefault(u => u.Username.Equals(name, StringComparison.CurrentCultureIgnoreCase));
            return user;
        }
    }
}
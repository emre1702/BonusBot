using BonusBot.Common.Defaults;
using BonusBot.Common.Interfaces.Commands;
using BonusBot.Common.Languages;
using Discord;
using Discord.Commands;
using Discord.Rest;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BonusBot.Common.Commands.TypeReaders
{
    public class IUserTypeReader : TypeReader
    {
        public override async Task<TypeReaderResult> ReadAsync(ICommandContext context, string input, IServiceProvider services)
        {
            try
            {
                var ctx = (ICustomCommandContext)context;
                IUser? user = null;
                if (MentionUtils.TryParseUser(input, out ulong userId)
                    || ulong.TryParse(input, out userId))
                {
                    if (userId == 0) return TypeReaderResult.FromError(CommandError.ParseFailed, Texts.CommandInvalidUserError);
                    user ??= await ctx.GetUserAsync(userId).ConfigureAwait(false);
                }

                if (ctx.Guild is SocketGuild socketGuild)
                {
                    user ??= await GetSocketGuildUserByName(socketGuild, input);
                    user ??= await GetUserByBan(socketGuild, input);
                }

                Thread.CurrentThread.CurrentUICulture = ctx.BonusGuild?.Settings.CultureInfo ?? Constants.DefaultCultureInfo;
                return user is { }
                    ? TypeReaderResult.FromSuccess(user)
                    : TypeReaderResult.FromError(CommandError.ParseFailed, Texts.CommandInvalidUserError);
            }
            catch
            {
                return TypeReaderResult.FromError(CommandError.ParseFailed, Texts.CommandInvalidUserError);
            }
        }

        private async Task<IUser?> GetSocketGuildUserByName(SocketGuild guild, string input)
        {
            var users = guild.GetUsersAsync().SelectMany<IReadOnlyCollection<IGuildUser>, IGuildUser>(e => e.ToAsyncEnumerable());
            return await GetUser(users, input);
        }

        private async Task<IUser?> GetUserByBan(SocketGuild guild, string input)
        {
            var users = guild.GetBansAsync()
                .SelectMany<IReadOnlyCollection<RestBan>, RestUser>(b => b.Select(e => e.User).ToAsyncEnumerable());
            return await GetUser(users, input);
        }

        private async Task<IUser?> GetUser(IAsyncEnumerable<IUser> users, string name)
        {
            IUser? user = await users.OfType<SocketGuildUser>().FirstOrDefaultAsync(u => u.Nickname?.Equals(name, StringComparison.CurrentCultureIgnoreCase) == true);
            user ??= await users.FirstOrDefaultAsync(u => u.Username.Equals(name, StringComparison.CurrentCultureIgnoreCase));
            user ??= await users.FirstOrDefaultAsync(u => (u.Username + "#" + u.Discriminator).Equals(name, StringComparison.CurrentCultureIgnoreCase));
            user ??= await users.FirstOrDefaultAsync(u => u.Username.Equals(name, StringComparison.CurrentCultureIgnoreCase));
            return user;
        }
    }
}
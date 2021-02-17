using BonusBot.Common.Models;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using System.Linq;
using System.Threading.Tasks;

namespace BonusBot.Common.Helper
{
    public static class TypeConvertHelper
    {
        public static async ValueTask<T?> ConvertTo<T>(this object value, DiscordSocketClient? client = null, IGuild? guild = null)
            => (T?)(object)(default(TokenOf<T>) switch
            {
                TokenOf<SocketGuild> => GetSocketGuild(value, client),
                TokenOf<IGuild> => GetSocketGuild(value, client),

                TokenOf<IGuildUser> => await GetIGuildUser(value, client, guild),
                TokenOf<SocketGuildUser> => GetSocketGuildUser(value, guild as SocketGuild),

                TokenOf<SocketGuildChannel> => GetSocketGuildChannel(value, guild as SocketGuild),
                TokenOf<SocketTextChannel> => GetSocketGuildTextChannel(value, client, guild as SocketGuild),

                TokenOf<Emote> => await GetEmote(value, guild),
                TokenOf<IEmote> => await GetIEmote(value, guild),

                _ => default
            });

        private static SocketGuild? GetSocketGuild(object value, DiscordSocketClient? client)
        {
            if (value is SocketGuild valueGuild)
                return valueGuild;
            if (client is null)
                return null;

            var valueStr = value.ToString();
            if (ulong.TryParse(valueStr, out var guildId))
                return client.GetGuild(guildId);

            return client.Guilds.FirstOrDefault(g => g.Name == valueStr);
        }

        private static async ValueTask<IGuildUser?> GetIGuildUser(object value, DiscordSocketClient? client, IGuild? guild)
        {
            if (value is SocketGuildUser valueGuildUser)
                return valueGuildUser;
            if (guild is null)
                return null;

            var valueStr = value.ToString();
            if (ulong.TryParse(valueStr, out var userId))
            {
                var user = (guild as SocketGuild)?.GetUser(userId);
                if (user is { })
                    return user;

                var restUser = await (client?.Rest.GetGuildUserAsync(guild.Id, userId) ?? Task.FromResult<RestGuildUser?>(null));
                if (restUser is { })
                    return restUser;
            }

            var userSearch = await guild.SearchUsersAsync(value.ToString(), 1);
            return userSearch.FirstOrDefault();
        }

        private static SocketGuildUser? GetSocketGuildUser(object value, SocketGuild? guild)
        {
            if (value is SocketGuildUser valueGuildUser)
                return valueGuildUser;
            if (guild is null)
                return null;

            var valueStr = value.ToString();
            if (ulong.TryParse(valueStr, out var userId))
            {
                var user = guild.GetUser(userId);
                if (user is { })
                    return user;
            }

            return null;
        }

        private static SocketGuildChannel? GetSocketGuildChannel(object value, SocketGuild? guild)
        {
            if (value is SocketGuildChannel valueGuildChannel)
                return valueGuildChannel;
            if (guild is null)
                return null;

            var valueStr = value.ToString();
            if (ulong.TryParse(valueStr, out var channelId))
                return guild.GetChannel(channelId);

            return null;
        }

        private static SocketTextChannel? GetSocketGuildTextChannel(object value, DiscordSocketClient? client, SocketGuild? guild)
        {
            if (value is SocketTextChannel valueTextChannel)
                return valueTextChannel;

            var valueStr = value.ToString();
            if (ulong.TryParse(valueStr, out var channelId))
            {
                var guildTextChannel = guild?.GetTextChannel(channelId);
                if (guildTextChannel is { })
                    return guildTextChannel;
                var textChannel = client?.GetChannel(channelId) as SocketTextChannel;
                if (textChannel is { })
                    return textChannel;
            }

            return null;
        }

        private static async ValueTask<Emote?> GetEmote(object value, IGuild? guild)
        {
            if (value is Emote valueEmote)
                return valueEmote;

            if (guild is null)
                return null;

            var valueStr = value.ToString();
            if (ulong.TryParse(valueStr, out var emoteId))
            {
                var emote = guild.Emotes.FirstOrDefault(e => e.Id == emoteId);
                if (emote is { })
                    return emote;

                emote = await guild.GetEmoteAsync(emoteId);
                return emote;
            }

            if (Emote.TryParse(valueStr, out var parsedEmote))
                return parsedEmote;

            return null;
        }

        private static async ValueTask<IEmote?> GetIEmote(object value, IGuild? guild)
        {
            if (value is IEmote valueEmote)
                return valueEmote;

            var emote = await GetEmote(value, guild);
            if (emote is { })
                return emote;

            return new Emoji(value.ToString());
        }
    }
}
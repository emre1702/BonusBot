using Discord;
using Discord.WebSocket;
using BonusBot.GamePlaningModule.Language;
using BonusBot.Services.DiscordNet;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BonusBot.GamePlaningModule
{
    internal static class Helpers
    {
        internal static string GetWithoutParticipantsMessage(string msg)
        {
            var index = msg.IndexOf(ModuleTexts.MeetupAnnouncementParticipantsTitle);
            if (index < 0)
                return msg.TrimEnd();

            return msg[0..(index - 1)].TrimEnd();
        }

        internal static async Task<string> GetRestUserName(SocketClientHandler socketClientHandler, ulong guildId, ulong userId)
        {
            var client = await socketClientHandler.ClientSource.Task;
            var user = await client.Rest.GetGuildUserAsync(guildId, userId);
            return user.Nickname;
        }

        internal static async IAsyncEnumerable<string> GetUserNames(SocketClientHandler socketClientHandler, IEnumerable<IUser> reactedUsers, SocketGuildChannel channel)
        {
            foreach (var user in reactedUsers.Where(u => !u.IsBot))
            {
                var guildUser = channel.Guild.GetUser(user.Id);
                if (guildUser is { })
                    yield return guildUser.Nickname;
                else
                    yield return await GetRestUserName(socketClientHandler, channel.Guild.Id, user.Id);
            }
        }
    }
}
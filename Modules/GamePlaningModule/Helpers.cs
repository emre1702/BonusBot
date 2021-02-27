using BonusBot.Common.Extensions;
using BonusBot.Common.Interfaces.Services;
using BonusBot.GamePlaningModule.Language;
using BonusBot.GamePlaningModule.Models;
using Discord;
using Discord.WebSocket;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BonusBot.GamePlaningModule
{
    internal static class Helpers
    {
        internal static string GetWithoutParticipantsMessage(string msg)
        {
            var index = msg.IndexOf(ModuleTexts.Participants);
            if (index < 0)
                return msg.TrimEnd();

            return msg[0..(index - 1)].TrimEnd();
        }

        internal static async Task<string> GetRestUserName(IDiscordClientHandler discordClientHandler, ulong guildId, ulong userId)
        {
            var client = await discordClientHandler.ClientSource.Task;
            var user = await client.Rest.GetGuildUserAsync(guildId, userId);
            return user.Nickname;
        }

        internal static async IAsyncEnumerable<string> GetUserNames(IDiscordClientHandler discordClientHandler, IEnumerable<IUser> reactedUsers, SocketGuild guild)
        {
            foreach (var user in reactedUsers.Where(u => !u.IsBot))
            {
                var guildUser = guild.GetUser(user.Id);
                if (guildUser is { })
                    yield return guildUser.Nickname ?? guildUser.Username;
                else
                {
                    var restUserName = await GetRestUserName(discordClientHandler, guild.Id, user.Id);
                    if (restUserName is { })
                        yield return restUserName;
                    else
                        yield return user.Username;
                }
            }
        }

        internal static Task<IEnumerable<IUser>> GetReactedUsers(IMessage message, Emote? emote)
            => emote is { }
            ? message.GetReactionUsersAsync(emote, 100).FlattenAsync()
            : Task.FromResult(new List<IUser>() as IEnumerable<IUser>);

        internal static async Task<List<string>> GetReactedUserNames(IMessage message, Emote? emote, SocketGuild guild, IDiscordClientHandler discordClientHandler)
        {
            var reactedUsers = await GetReactedUsers(message, emote);
            return await GetUserNames(discordClientHandler, reactedUsers, guild).ToListAsync();
        }

        internal static EmbedBuilder CreateAnnouncementEmbedBuilder(AnnouncementEmbedData data)
            => new EmbedBuilder()
                .WithColor(Color.Green)
                .WithCurrentTimestamp()
                .WithDescription(string.Format(ModuleTexts.MeetupAnnouncementDescription, data.ParticipationEmoteString, data.LateParticipationEmoteString, data.MaybeParticipationEmoteString, data.CancellationEmoteString))
                .WithFields(
                    new() { Name = ModuleTexts.Game + ":", Value = data.Game, IsInline = true },
                    new() { Name = ModuleTexts.DateTime + ":", Value = data.DateTimeStr, IsInline = true },
                    new() { Name = ModuleTexts.Participants + $" ({data.AmountParticipants}):", Value = data.ParticipantsString, IsInline = false },
                    new() { Name = ModuleTexts.LateParticipants + $" ({data.AmountLateParticipants}):", Value = data.LateParticipantsString, IsInline = false },
                    new() { Name = ModuleTexts.MaybeParticipants + $" ({data.AmountMaybeParticipants}):", Value = data.MaybeParticipantsString, IsInline = false },
                    new() { Name = ModuleTexts.Cancellations + $" ({data.AmountCancellations}):", Value = data.CancellationsString, IsInline = false }
                )

                .WithFooter(GetAnnouncementFooter())

                .WithTitle(ModuleTexts.MeetupAnnouncementTitle);

        internal static string GetAnnouncementFooter()
            => typeof(Helpers).GetModuleName() + " Announcement";
    }
}
using Discord;
using Discord.WebSocket;
using BonusBot.GamePlaningModule.Language;
using BonusBot.Services.DiscordNet;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BonusBot.Common.Extensions;
using BonusBot.Common.Languages;
using System;

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

        internal static EmbedBuilder CreateAnnouncementEmbedBuilder(string game, string dateTimeStr, string participantsString, int amountParticipants, IEmote emote)
            => new EmbedBuilder()
                .WithColor(Color.Green)
                .WithCurrentTimestamp()
                .WithDescription(string.Format(ModuleTexts.MeetupAnnouncementDescription, emote.ToString()))
                .WithFields(
                    new() { Name = ModuleTexts.Game + ":", Value = game, IsInline = true },
                    new() { Name = ModuleTexts.DateTime + ":", Value = dateTimeStr, IsInline = true },
                    new() { Name = ModuleTexts.Participants + $" ({amountParticipants}):", Value = !string.IsNullOrWhiteSpace(participantsString) ? participantsString : "-", IsInline = false })
                .WithFooter(GetAnnouncementFooter())
                .WithTitle(ModuleTexts.MeetupAnnouncementTitle);

        internal static string GetAnnouncementFooter()
            => typeof(Helpers).Assembly.GetName().Name!.ToModuleName() + " Announcement";
    }
}
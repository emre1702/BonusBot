using Discord;
using Discord.WebSocket;
using BonusBot.Common.Defaults;
using BonusBot.GamePlaningModule.Language;
using BonusBot.Services.DiscordNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BonusBot.GamePlaningModule
{
    public partial class GamePlaning
    {
        private readonly SocketClientHandler _socketClientHandler;

        public GamePlaning(SocketClientHandler socketClientHandler)
        {
            _socketClientHandler = socketClientHandler;
            AddEvents(socketClientHandler);
        }

        private async void AddEvents(SocketClientHandler socketClientHandler)
        {
            var client = await socketClientHandler.ClientSource.Task;
            ModuleTexts.Culture = Constants.Culture;

            client.ReactionAdded += SetParticipantsToMessage;
            client.ReactionRemoved += SetParticipantsToMessage;
        }

        private async Task SetParticipantsToMessage(Cacheable<IUserMessage, ulong> cachedMessageOrId, ISocketMessageChannel channel, SocketReaction reaction)
        {
            lock (_announcementMessageIds)
            {
                if (!_announcementMessageIds.Contains(reaction.MessageId))
                    return;
            }
            if (_lastAnnouncementEmote?.Id != (reaction.Emote as Emote)?.Id)
                return;
            var message = await cachedMessageOrId.DownloadAsync();
            if (!message.Author.IsBot)
                return;

            var reactedUsers = await message.GetReactionUsersAsync(reaction.Emote, 100).FlattenAsync();
            var guildChannel = (SocketGuildChannel)channel;

            var names = await Helpers.GetUserNames(_socketClientHandler, reactedUsers, guildChannel).ToListAsync();
            var namesStr = string.Join(", ", names);
            var msgText = Helpers.GetWithoutParticipantsMessage(message.Content);

            await message.ModifyAsync(prop => prop.Content = msgText
                + Environment.NewLine + Environment.NewLine
                + ModuleTexts.MeetupAnnouncementParticipantsTitle + $" ({names.Count})"
                + Environment.NewLine
                + namesStr);
        }
    }
}
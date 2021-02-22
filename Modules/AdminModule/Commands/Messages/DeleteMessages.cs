using BonusBot.AdminModule.Models.CommandArgs;
using BonusBot.Common.Commands;
using Discord;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using BonusBot.AdminModule.Languages;

namespace BonusBot.AdminModule.Commands.Messages
{
    internal class DeleteMessages : CommandHandlerBase<Main, DeleteMessagesArgs>
    {
        public DeleteMessages(Main main) : base(main)
        {
        }

        public override async Task Do(DeleteMessagesArgs args)
        {
            var messages = args.User is { }
                ? await GetLastMessagesByUser(args.Limit, args.User).ConfigureAwait(false)
                : await GetLastMessages(args.Limit).ConfigureAwait(false);

            var amountMessages = messages.Count;
            await RemoveMessages(messages).ConfigureAwait(false);

            await Class.ReplyAsync(string.Format(ModuleTexts.DeletedMessagesInfo, amountMessages));
        }

        private async Task<List<IMessage>> GetLastMessages(int limit)
        {
            var messages = new List<IMessage>();
            IMessage lastMessage = Class.Context.Message;
            while (limit >= 0)
            {
                var currentLimit = Math.Min(limit, DiscordConfig.MaxMessagesPerBatch);
                limit -= currentLimit;
                var fetchedMessages = await Class.Context.Channel.GetMessagesAsync(lastMessage, Direction.Before, currentLimit, CacheMode.AllowDownload).FlattenAsync().ConfigureAwait(false);
                if (!fetchedMessages.Any())
                    break;
                lastMessage = fetchedMessages.Last();
                messages.AddRange(fetchedMessages);
            }

            return messages;
        }

        private async Task<List<IMessage>> GetLastMessagesByUser(int limit, IUser user)
        {
            var messages = new List<IMessage>();
            IMessage lastMessage = Class.Context.Message;
            while (limit >= 0)
            {
                var currentLimit = Math.Min(limit * 5, DiscordConfig.MaxMessagesPerBatch);
                var fetchedMessages = await Class.Context.Channel.GetMessagesAsync(lastMessage, Direction.Before, currentLimit, CacheMode.AllowDownload).FlattenAsync().ConfigureAwait(false);
                if (!fetchedMessages.Any())
                    break;
                lastMessage = fetchedMessages.Last();
                var userMessages = fetchedMessages.Where(m => m.Author.Id == user.Id).ToList();
                if (userMessages.Count == 0)
                    continue;
                if (userMessages.Count > limit)
                    userMessages = userMessages.Take(limit).ToList();
                messages.AddRange(userMessages);
                limit -= userMessages.Count;
            }

            return messages;
        }

        private async Task RemoveMessages(List<IMessage> messages)
        {
            var dateTimeUtcNow = DateTime.UtcNow.AddMinutes(5);
            await BulkRemoveMessages(messages, dateTimeUtcNow).ConfigureAwait(false);
            await RemoveTooOldMessages(messages, dateTimeUtcNow).ConfigureAwait(false);
        }

        private Task BulkRemoveMessages(List<IMessage> messages, DateTime dateTimeUtcNow)
        {
            var messagesToBulkRemove = messages.Where(m => (dateTimeUtcNow - m.CreatedAt).TotalDays < 14);
            return ((ITextChannel)Class.Context.Channel).DeleteMessagesAsync(messagesToBulkRemove);
        }

        private Task RemoveTooOldMessages(List<IMessage> messages, DateTime dateTimeUtcNow)
        {
            var messagesTooOldForBulkRemoveTasks = messages
                .Where(m => (dateTimeUtcNow - m.CreatedAt).TotalDays >= 14)
                .Select(m => m.DeleteAsync());
            return Task.WhenAll(messagesTooOldForBulkRemoveTasks);
        }
    }
}
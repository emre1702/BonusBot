using Discord;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BonusBot.WebDashboardModule.Discord
{
    public class WebMessage : IUserMessage
    {
        public IUserMessage ReferencedMessage => throw new NotImplementedException();

        public MessageType Type => MessageType.Default;

        public MessageSource Source => MessageSource.User;

        public bool IsTTS => false;

        public bool IsPinned => false;

        public bool IsSuppressed => false;

        public bool MentionedEveryone => false;

        public string Content { get; }

        public DateTimeOffset Timestamp => DateTimeOffset.UtcNow;

        public DateTimeOffset? EditedTimestamp => throw new NotImplementedException();

        public IMessageChannel Channel { get; }
        public IUser Author { get; }

        public IReadOnlyCollection<IAttachment> Attachments => throw new NotImplementedException();

        public IReadOnlyCollection<IEmbed> Embeds => throw new NotImplementedException();

        public IReadOnlyCollection<ITag> Tags => throw new NotImplementedException();

        public IReadOnlyCollection<ulong> MentionedChannelIds => throw new NotImplementedException();

        public IReadOnlyCollection<ulong> MentionedRoleIds => throw new NotImplementedException();

        public IReadOnlyCollection<ulong> MentionedUserIds => throw new NotImplementedException();

        public MessageActivity Activity => throw new NotImplementedException();

        public MessageApplication Application => throw new NotImplementedException();

        public MessageReference Reference => throw new NotImplementedException();

        public IReadOnlyDictionary<IEmote, ReactionMetadata> Reactions => throw new NotImplementedException();

        public MessageFlags? Flags => MessageFlags.None;

        public DateTimeOffset CreatedAt => DateTimeOffset.UtcNow;

        public ulong Id => ulong.MaxValue;

        public IReadOnlyCollection<ISticker> Stickers => throw new NotImplementedException();

        public WebMessage(string content, IUser author, IMessageChannel channel)
        {
            Content = content;
            Author = author;
            Channel = channel;
        }

        public Task AddReactionAsync(IEmote emote, RequestOptions? options = null)
        {
            throw new NotImplementedException();
        }

        public Task CrosspostAsync(RequestOptions? options = null)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(RequestOptions? options = null)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<IReadOnlyCollection<IUser>> GetReactionUsersAsync(IEmote emoji, int limit, RequestOptions? options = null)
        {
            throw new NotImplementedException();
        }

        public Task ModifyAsync(Action<MessageProperties> func, RequestOptions? options = null)
        {
            throw new NotImplementedException();
        }

        public Task ModifySuppressionAsync(bool suppressEmbeds, RequestOptions? options = null)
        {
            throw new NotImplementedException();
        }

        public Task PinAsync(RequestOptions? options = null)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAllReactionsAsync(RequestOptions? options = null)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAllReactionsForEmoteAsync(IEmote emote, RequestOptions? options = null)
        {
            throw new NotImplementedException();
        }

        public Task RemoveReactionAsync(IEmote emote, IUser user, RequestOptions? options = null)
        {
            throw new NotImplementedException();
        }

        public Task RemoveReactionAsync(IEmote emote, ulong userId, RequestOptions? options = null)
        {
            throw new NotImplementedException();
        }

        public string Resolve(TagHandling userHandling = TagHandling.Name, TagHandling channelHandling = TagHandling.Name, TagHandling roleHandling = TagHandling.Name, TagHandling everyoneHandling = TagHandling.Ignore, TagHandling emojiHandling = TagHandling.Name)
        {
            throw new NotImplementedException();
        }

        public Task UnpinAsync(RequestOptions? options = null)
        {
            throw new NotImplementedException();
        }
    }
}

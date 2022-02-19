using BonusBot.WebDashboardModule.Models.Discord;
using Discord;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace BonusBot.WebDashboardModule.Discord
{
    public class WebUser : IUser
    {
        public virtual string AvatarId => _userData.Avatar ?? string.Empty;

        public virtual string Discriminator => _userData.Discriminator;

        public virtual ushort DiscriminatorValue => ushort.Parse(_userData.Discriminator);

        public virtual bool IsBot => _userData.IsBot ?? false;

        public virtual bool IsWebhook => false;

        public string Username => _userData.Username;

        public virtual UserProperties? PublicFlags => (UserProperties?)_userData.PublicFlags;

        public virtual DateTimeOffset CreatedAt => DateTimeOffset.UtcNow;

        public ulong Id => _userData.Id;

        public virtual string Mention => MentionUtils.MentionUser(_userData.Id);

        public virtual IActivity Activity => throw new NotImplementedException();

        public virtual UserStatus Status => throw new NotImplementedException();

        public virtual IReadOnlyCollection<ClientType> ActiveClients => throw new NotImplementedException();

        public virtual IReadOnlyCollection<IActivity> Activities => throw new NotImplementedException();
        public List<string> Messages { get; } = new();
        public List<string> Errors { get; } = new();

        public virtual string GetAvatarUrl(ImageFormat format = ImageFormat.Auto, ushort size = 128)
        {
            throw new NotImplementedException();
        }

        public string GetDefaultAvatarUrl()
        {
            throw new NotImplementedException();
        }

        public Task<IDMChannel> CreateDMChannelAsync(RequestOptions? options = null)
            => Task.FromResult((IDMChannel)new WebDMChannel(this));

        public IUserMessage AddWebMessage(string? text, IMessageChannel channel)
        {
            if (text is not null)
                Messages.Add(text);
            return new WebMessage(text ?? string.Empty, this, channel);
        }

        public IUserMessage AddWebErrorMessage(string? text, IMessageChannel channel)
        {
            if (text is not null)
                Errors.Add(text);
            return new WebMessage(text ?? string.Empty, this, channel);
        }

        private readonly UserResponseData _userData;

        public WebUser(UserResponseData userData)
            => _userData = userData;
    }
}

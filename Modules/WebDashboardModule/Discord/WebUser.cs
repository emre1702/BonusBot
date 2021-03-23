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
        public string AvatarId => _userData.Avatar ?? string.Empty;

        public string Discriminator => _userData.Discriminator;

        public ushort DiscriminatorValue => ushort.Parse(_userData.Discriminator);

        public bool IsBot => _userData.IsBot ?? false;

        public bool IsWebhook => false;

        public string Username => _userData.Username;

        public UserProperties? PublicFlags => (UserProperties?)_userData.PublicFlags;

        public DateTimeOffset CreatedAt => DateTimeOffset.UtcNow;

        public ulong Id => _userData.Id;

        public string Mention => MentionUtils.MentionUser(_userData.Id);

        public IActivity Activity => throw new NotImplementedException();

        public UserStatus Status => throw new NotImplementedException();

        public IImmutableSet<ClientType> ActiveClients => throw new NotImplementedException();

        public IImmutableList<IActivity> Activities => throw new NotImplementedException();
        public List<string> Messages { get; } = new();

        public string GetAvatarUrl(ImageFormat format = ImageFormat.Auto, ushort size = 128)
        {
            throw new NotImplementedException();
        }

        public string GetDefaultAvatarUrl()
        {
            throw new NotImplementedException();
        }

        public Task<IDMChannel> GetOrCreateDMChannelAsync(RequestOptions? options = null)
            => Task.FromResult((IDMChannel)new WebDMChannel(this));

        public IUserMessage AddWebMessage(string? text, IMessageChannel channel)
        {
            if (text is not null)
                Messages.Add(text);
            return new WebMessage(text ?? string.Empty, this, channel);
        }

        private readonly UserResponseData _userData;

        public WebUser(UserResponseData userData)
            => _userData = userData;
    }
}

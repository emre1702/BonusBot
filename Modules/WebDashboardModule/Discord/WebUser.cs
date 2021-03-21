using Discord;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace BonusBot.WebDashboardModule.Discord
{
    public class WebUser : IUser
    {
        public string AvatarId => throw new NotImplementedException();

        public string Discriminator => throw new NotImplementedException();

        public ushort DiscriminatorValue => throw new NotImplementedException();

        public bool IsBot => throw new NotImplementedException();

        public bool IsWebhook => throw new NotImplementedException();

        public string Username => throw new NotImplementedException();

        public UserProperties? PublicFlags => throw new NotImplementedException();

        public DateTimeOffset CreatedAt => throw new NotImplementedException();

        public ulong Id => throw new NotImplementedException();

        public string Mention => throw new NotImplementedException();

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
    }
}

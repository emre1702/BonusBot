using BonusBot.WebDashboardModule.Models.Discord;
using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace BonusBot.WebDashboardModule.Discord
{
    public class WebGuildUser : WebUser, IGuildUser
    {
        public DateTimeOffset? JoinedAt => _guildUser.JoinedAt;

        public string Nickname => _guildUser.Nickname;

        public GuildPermissions GuildPermissions => _guildUser.GuildPermissions;

        public IGuild Guild => _guildUser.Guild;

        public ulong GuildId => _guildUser.Guild.Id;

        public DateTimeOffset? PremiumSince => _guildUser.PremiumSince;

        public IReadOnlyCollection<ulong> RoleIds => _guildUser.Roles.Select(r => r.Id).ToList();

        public bool? IsPending => _guildUser.IsPending;

        public bool IsDeafened => _guildUser.IsDeafened;

        public bool IsMuted => _guildUser.IsMuted;

        public bool IsSelfDeafened => _guildUser.IsSelfDeafened;

        public bool IsSelfMuted => _guildUser.IsSelfMuted;

        public bool IsSuppressed => _guildUser.IsSuppressed;

        public IVoiceChannel VoiceChannel => _guildUser.VoiceChannel;

        public string VoiceSessionId => _guildUser.VoiceSessionId;

        public bool IsStreaming => _guildUser.IsStreaming;

        public override IImmutableSet<ClientType> ActiveClients => _guildUser.ActiveClients;
        public override IImmutableList<IActivity> Activities => _guildUser.Activities;
        public override IActivity Activity => _guildUser.Activity;
        public override string AvatarId => _guildUser.AvatarId;
        public override DateTimeOffset CreatedAt => _guildUser.CreatedAt;
        public override string Discriminator => _guildUser.Discriminator;
        public override ushort DiscriminatorValue => _guildUser.DiscriminatorValue;
        public override bool IsBot => _guildUser.IsBot;
        public override bool IsWebhook => _guildUser.IsWebhook;
        public override string Mention => _guildUser.Mention;
        public override UserProperties? PublicFlags => _guildUser.PublicFlags;
        public override UserStatus Status => _guildUser.Status;

        private readonly SocketGuildUser _guildUser;

        public WebGuildUser(UserResponseData userData, SocketGuildUser guildUser): base(userData)
        {
            _guildUser = guildUser;
        }

        public Task AddRoleAsync(IRole role, RequestOptions? options = null)
            => _guildUser.AddRoleAsync(role, options);

        public Task AddRolesAsync(IEnumerable<IRole> roles, RequestOptions? options = null)
            => _guildUser.AddRolesAsync(roles, options);

        public ChannelPermissions GetPermissions(IGuildChannel channel)
            => _guildUser.GetPermissions(channel);

        public Task KickAsync(string? reason = null, RequestOptions? options = null)
            => _guildUser.KickAsync(reason, options);

        public Task ModifyAsync(Action<GuildUserProperties> func, RequestOptions? options = null)
            => _guildUser.ModifyAsync(func, options);

        public Task RemoveRoleAsync(IRole role, RequestOptions? options = null)
            => _guildUser.RemoveRoleAsync(role, options);

        public Task RemoveRolesAsync(IEnumerable<IRole> roles, RequestOptions? options = null)
            => _guildUser.RemoveRolesAsync(roles, options);

        public override string GetAvatarUrl(ImageFormat format = ImageFormat.Auto, ushort size = 128)
            => _guildUser.GetAvatarUrl(format, size);

        public Task AddRoleAsync(ulong roleId, RequestOptions? options = null)
            => _guildUser.AddRoleAsync(roleId, options);

        public Task AddRolesAsync(IEnumerable<ulong> roleIds, RequestOptions? options = null)
            => _guildUser.AddRolesAsync(roleIds, options);

        public Task RemoveRoleAsync(ulong roleId, RequestOptions? options = null)
            => _guildUser.RemoveRoleAsync(roleId, options);

        public Task RemoveRolesAsync(IEnumerable<ulong> roleIds, RequestOptions? options = null)
            => _guildUser.RemoveRolesAsync(roleIds, options);
    }
}

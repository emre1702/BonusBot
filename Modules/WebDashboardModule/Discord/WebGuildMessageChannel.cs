using Discord;
using Discord.Audio;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BonusBot.WebDashboardModule.Discord
{
    public class WebGuildMessageChannel : WebDMChannel, IMessageChannel, IGuildChannel, IGroupChannel
    {
        public override string Name => "WebMessageChannel";

        public int Position => throw new NotImplementedException();

        public IGuild Guild => _guild;

        public ulong GuildId => _guild.Id;

        public IReadOnlyCollection<Overwrite> PermissionOverwrites => throw new NotImplementedException();

        private readonly SocketGuild _guild;

        public WebGuildMessageChannel(WebUser user, SocketGuild guild) : base(user)
            => (_guild) = (guild);

        public Task ModifyAsync(Action<GuildChannelProperties> func, RequestOptions? options = null)
        {
            throw new NotImplementedException();
        }

        public OverwritePermissions? GetPermissionOverwrite(IRole role)
        {
            throw new NotImplementedException();
        }

        public OverwritePermissions? GetPermissionOverwrite(IUser user)
        {
            throw new NotImplementedException();
        }

        public Task RemovePermissionOverwriteAsync(IRole role, RequestOptions? options = null)
        {
            throw new NotImplementedException();
        }

        public Task RemovePermissionOverwriteAsync(IUser user, RequestOptions? options = null)
        {
            throw new NotImplementedException();
        }

        public Task AddPermissionOverwriteAsync(IRole role, OverwritePermissions permissions, RequestOptions? options = null)
        {
            throw new NotImplementedException();
        }

        public Task AddPermissionOverwriteAsync(IUser user, OverwritePermissions permissions, RequestOptions? options = null)
        {
            throw new NotImplementedException();
        }

        IAsyncEnumerable<IReadOnlyCollection<IGuildUser>> IGuildChannel.GetUsersAsync(CacheMode mode, RequestOptions options)
        {
            throw new NotImplementedException();
        }

        Task<IGuildUser> IGuildChannel.GetUserAsync(ulong id, CacheMode mode, RequestOptions options)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(RequestOptions? options = null)
        {
            throw new NotImplementedException();
        }

        public Task LeaveAsync(RequestOptions? options = null)
        {
            throw new NotImplementedException();
        }

        public Task<IAudioClient> ConnectAsync(bool selfDeaf = false, bool selfMute = false, bool external = false)
        {
            throw new NotImplementedException();
        }

        public Task DisconnectAsync()
        {
            throw new NotImplementedException();
        }
    }
}

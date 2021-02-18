using BonusBot.Common.Interfaces.Guilds;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace GuildsSystem
{
    public class Provider : IBonusGuildProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public Provider(IServiceProvider serviceProvider)
            => _serviceProvider = serviceProvider;

        public async Task<IBonusGuild> Create(SocketGuild discordGuild)
        {
            var guild = ActivatorUtilities.CreateInstance<Guild>(_serviceProvider);
            await guild.Initialize(discordGuild);

            return guild;
        }
    }
}
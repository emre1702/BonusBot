using BonusBot.Common.Interfaces.Guilds;
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

        public async Task<IBonusGuild> Create()
        {
            var guild = ActivatorUtilities.CreateInstance<Guild>(_serviceProvider);
            await guild.Initialize();

            return guild;
        }
    }
}
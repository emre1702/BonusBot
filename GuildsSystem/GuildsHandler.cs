using BonusBot.Common.Enums;
using BonusBot.Common.Events.Arguments;
using BonusBot.Common.Helper;
using BonusBot.Common.Interfaces.Guilds;
using BonusBot.Services.Events;
using Discord;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("BonusBot.Core")]

namespace BonusBot.GuildsSystem
{
    internal class GuildsHandler : IGuildsHandler
    {
        private readonly Dictionary<ulong, IBonusGuild> _guildsInitialized = new();
        private readonly SemaphoreSlim _initializeSemaphore = new(1, 1);

        private readonly IServiceProvider _serviceProvider;

        public GuildsHandler(EventsHandler eventsHandler, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            eventsHandler.GuildAvailable += InitGuild;
        }

        public IBonusGuild? GetGuild(ulong? guildId)
        {
            if (guildId is null) return null;
            lock (_guildsInitialized)
            {
                _guildsInitialized.TryGetValue(guildId.Value, out var guild);
                return guild;
            }
        }

        public IBonusGuild? GetGuild(IGuild? guild)
            => GetGuild(guild?.Id);

        private async Task InitGuild(ClientGuildArg arg)
        {
            await _initializeSemaphore.WaitAsync();

            try
            {
                lock (_guildsInitialized)
                {
                    if (_guildsInitialized.ContainsKey(arg.Guild.Id))
                        return;
                }

                var guild = ActivatorUtilities.CreateInstance<Guild>(_serviceProvider);
                await guild.Initialize(arg.Guild);

                lock (_guildsInitialized) _guildsInitialized[arg.Guild.Id] = guild;
                ConsoleHelper.Log(LogSeverity.Info, LogSource.Discord, $"Initialized Guild '{arg.Guild.Name}'.");
            }
            finally
            {
                _initializeSemaphore.Release();
            }
        }
    }
}
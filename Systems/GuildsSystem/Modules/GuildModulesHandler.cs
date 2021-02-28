using BonusBot.Common;
using BonusBot.Common.Defaults;
using BonusBot.Common.Extensions;
using BonusBot.Common.Interfaces.Guilds;
using BonusBot.Common.Interfaces.Services;
using BonusBot.Database;
using BonusBot.Database.Entities.Settings;
using Discord.WebSocket;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BonusBot.GuildsSystem.Modules
{
    internal class GuildModulesHandler : IGuildModulesHandler
    {
        private readonly HashSet<Assembly> _activeModuleAssemblies = new();

#nullable disable
        private IGuildSettingsHandler _settingsHandler;
#nullable restore

        private readonly BonusDbContextFactory _dbContextFactory;
        private readonly IModulesHandler _modulesHandler;

        public GuildModulesHandler(BonusDbContextFactory dbContextFactory, IModulesHandler modulesHandler)
            => (_dbContextFactory, _modulesHandler) = (dbContextFactory, modulesHandler);

        public Task Init(IGuildSettingsHandler settingsHandler, SocketGuild discordGuild)
        {
            _settingsHandler = settingsHandler;
            return LoadActivatedModules(discordGuild.Id);
        }

        private async Task LoadActivatedModules(ulong guildId)
        {
            var deactivatedModuleNames = await LoadDeactivatedModuleNames(guildId);

            // Ensure the modules are all loaded
            await _modulesHandler.LoadModulesTaskSource.Task;
            lock (_activeModuleAssemblies)
            {
                _activeModuleAssemblies.Add(typeof(CommonSettings).Assembly);
                foreach (var moduleAssembly in _modulesHandler.LoadedModuleAssemblies)
                {
                    var moduleName = moduleAssembly.ToModuleName();
                    if (!deactivatedModuleNames.Contains(moduleName))
                        _activeModuleAssemblies.Add(moduleAssembly);
                }
            }
        }

        private async Task<HashSet<string>> LoadDeactivatedModuleNames(ulong guildId)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();
            var entries = await (dbContext.GuildsSettings as IQueryable<GuildsSettings>)
                .Where(s => s.GuildId == guildId && s.Key == Constants.ModuleDeactivatedDbKey)
                .Select(s => s.Module)
                .ToListAsync();
            return entries.ToHashSet();
        }

        public async ValueTask<bool> Add(Assembly assembly)
        {
            lock (_activeModuleAssemblies)
            {
                var added = _activeModuleAssemblies.Add(assembly);
                if (!added) return false;
            }
            await _settingsHandler.Remove(assembly, Constants.ModuleDeactivatedDbKey);
            return true;
        }

        public async ValueTask<bool> Remove(Assembly assembly)
        {
            if (assembly == typeof(CommonSettings).Assembly) return false;
            lock (_activeModuleAssemblies)
            {
                var removed = _activeModuleAssemblies.Remove(assembly);
                if (!removed) return false;
            }

            await _settingsHandler.Set(assembly, Constants.ModuleDeactivatedDbKey, true);
            return true;
        }

        public bool Contains(Assembly assembly)
        {
            lock (_activeModuleAssemblies)
            {
                return _activeModuleAssemblies.Contains(assembly);
            }
        }

        public Assembly? GetActivatedModuleAssembly(string moduleName)
        {
            lock (_activeModuleAssemblies)
            {
                return _activeModuleAssemblies.FirstOrDefault(a => a.ToModuleName() == moduleName);
            }
        }

        public List<Assembly> GetActivatedModuleAssemblies()
        {
            lock (_activeModuleAssemblies)
            {
                return _activeModuleAssemblies.ToList();
            }
        }

        public override string ToString()
        {
            lock (_activeModuleAssemblies)
            {
                return string.Join(", ", _activeModuleAssemblies.Select(a => a.ToModuleName()));
            }
        }
    }
}
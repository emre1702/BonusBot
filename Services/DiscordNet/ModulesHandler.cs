using BonusBot.Common;
using BonusBot.Common.Enums;
using BonusBot.Common.Extensions;
using BonusBot.Common.Helper;
using BonusBot.Common.Interfaces.Services;
using Discord;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;

namespace BonusBot.Services.DiscordNet
{
    internal class ModulesHandler : IModulesHandler
    {
        public List<Assembly> LoadedModuleAssemblies { get; } = new();
        public TaskCompletionSource LoadModulesTaskSource { get; } = new();

        private readonly IDiscordClientHandler _discordClientHandler;
        private readonly IServiceProvider _serviceProvider;
        private readonly ICommandsHandler _commandsHandler;
        

        public ModulesHandler(IDiscordClientHandler discordClientHandler, IServiceProvider serviceProvider, ICommandsHandler commandsHandler)
            => (_discordClientHandler, _serviceProvider, _commandsHandler) = (discordClientHandler, serviceProvider, commandsHandler);

        public Assembly? FindAssemblyByModuleName(string moduleName, bool includeCommon = true)
        {
            moduleName = moduleName.ToModuleName();
            if (includeCommon && moduleName.Equals(typeof(CommonSettings).GetModuleName()))
                return typeof(CommonSettings).Assembly;

            lock (LoadedModuleAssemblies)
            {
                return LoadedModuleAssemblies.FirstOrDefault(a => a.GetName().Name?.ToModuleName().Equals(moduleName, StringComparison.CurrentCultureIgnoreCase) == true);
            }
        }

        public async Task LoadModules()
        {
            await _discordClientHandler.ClientSource.Task;

            foreach (var assembly in GetAndLoadModuleAssemblies())
                await _commandsHandler.CommandService.AddModulesAsync(assembly, _serviceProvider);

            LoadModulesTaskSource.SetResult();
        }

        private IEnumerable<Assembly> GetAndLoadModuleAssemblies()
        {
            var execPath = Directory.GetCurrentDirectory();

            foreach (var file in Directory.EnumerateFiles(execPath, "*Module.dll", SearchOption.AllDirectories))
            {
                var context = new AssemblyLoadContext(file);
                var assembly = context.LoadFromAssemblyPath(file);

                var name = assembly.ToModuleName();
                lock (assembly)
                {
                    LoadedModuleAssemblies.Add(assembly);
                }
                ConsoleHelper.Log(LogSeverity.Info, LogSource.Core, $"Loaded {name} v{assembly.GetName().Version} assembly.");
                yield return assembly;
            }
        }
    }
}
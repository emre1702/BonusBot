using Discord;
using Discord.Commands;
using BonusBot.Common.Enums;
using BonusBot.Common.Extensions;
using BonusBot.Common.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace BonusBot.Services.DiscordNet
{
    public class ModulesHandler
    {
        public List<Assembly> LoadedModuleAssemblies = new();

        public ModulesHandler(SocketClientHandler socketClientHandler, IServiceProvider serviceProvider, CommandsHandler commandsHandler)
        {
            AddModules(socketClientHandler, serviceProvider, commandsHandler);
        }

        public Assembly? FindAssemblyByModuleName(string moduleName)
        {
            lock (LoadedModuleAssemblies)
            {
                return LoadedModuleAssemblies.FirstOrDefault(a => a.GetName().Name?.ToModuleName().Equals(moduleName.ToModuleName(), StringComparison.CurrentCultureIgnoreCase) == true);
            }
        }

        private async void AddModules(SocketClientHandler socketClientHandler, IServiceProvider serviceProvider, CommandsHandler commandsHandler)
        {
            await socketClientHandler.ClientSource.Task;

            foreach (var assembly in GetModuleAssemblies())
                await commandsHandler.CommandService.AddModulesAsync(assembly, serviceProvider);
        }

        private IEnumerable<Assembly> GetModuleAssemblies()
        {
            var execPath = Directory.GetCurrentDirectory();

            foreach (var file in Directory.EnumerateFiles(execPath, "*Module.dll", SearchOption.AllDirectories))
            {
                var context = new AssemblyLoadContext(file, true);
                var assembly = context.LoadFromAssemblyPath(file);
                var name = assembly.GetName().Name!.ToModuleName();
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
using BonusBot.Common;
using BonusBot.Common.Commands.Conditions;
using BonusBot.Common.Extensions;
using BonusBot.Common.Interfaces.Services;
using BonusBot.ModulesControllingModule.Languages;
using Discord;
using Discord.Commands;
using System.Reflection;
using System.Threading.Tasks;

namespace ModulesControllingModule
{
    [RequireContext(ContextType.Guild)]
    [RequireUserPermission(GuildPermission.Administrator)]
    [Group("module")]
    [Alias("modul")]
    public class GuildModuleCommands : CommandBase
    {
        private readonly IModulesHandler _modulesHandler;

        public GuildModuleCommands(IModulesHandler modulesHandler)
            => _modulesHandler = modulesHandler;

        [Command("+")]
        [Alias("add", "hinzufügen", "activate", "aktivieren", "active", "aktiv", "plus", "adden", "enable")]
        public async Task AddModule(string moduleName)
        {
            var assembly = await CheckGetModuleAssembly(moduleName);
            if (assembly is null)
                return;
            if (await Context.BonusGuild!.Modules.Add(assembly))
                await ReplyToUserAsync(string.Format(ModuleTexts.ModuleHasBeenEnabledForGuild, assembly.ToModuleName()));
        }

        [Command("-")]
        [Alias("remove", "entfernen", "deactivate", "deaktivieren", "inactive", "inaktiv", "minus", "removen", "disable")]
        public async Task RemoveModule(string moduleName)
        {
            var assembly = await CheckGetModuleAssembly(moduleName);
            if (assembly is null)
                return;
            if (assembly == typeof(CommonSettings).Assembly)
                return;
            if (await Context.BonusGuild!.Modules.Remove(assembly))
                await ReplyToUserAsync(string.Format(ModuleTexts.ModuleHasBeenDisabledForGuild, assembly.ToModuleName()));
        }

        private async ValueTask<Assembly?> CheckGetModuleAssembly(string moduleName)
        {
            var moduleAssembly = _modulesHandler.FindAssemblyByModuleName(moduleName, false);
            if (moduleAssembly is null)
            {
                await ReplyToUserAsync(string.Format(ModuleTexts.ModuleDoesNotExist, moduleName));
                return null;
            }
            return moduleAssembly;
        }
    }
}
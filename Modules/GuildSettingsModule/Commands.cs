using Discord;
using Discord.Commands;
using BonusBot.Common.Defaults;
using BonusBot.Common.Extensions;
using BonusBot.Database;
using BonusBot.Database.Entities.Settings;
using BonusBot.GuildSettingsModule.Language;
using BonusBot.Services.DiscordNet;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace GuildSettingsModule
{
    [Group("config")]
    [Alias("settings", "setting")]
    public class GuildSettings : CommandBase
    {
        private readonly ModulesHandler _modulesHandler;
        private readonly FunDbContextFactory _dbContextFactory;

        public GuildSettings(ModulesHandler modulesHandler, FunDbContextFactory dbContextFactory)
        {
            _modulesHandler = modulesHandler;
            _dbContextFactory = dbContextFactory;

            ModuleTexts.Culture = Constants.Culture;
        }

        [RequireContext(ContextType.Guild)]
        [RequireUserPermission(GuildPermission.Administrator)]
        [Command]
        public async Task Set(string moduleName, string key, [Remainder] string value)
        {
            if (!Helpers.DoesSettingExists(_modulesHandler, key, moduleName))
            {
                await ReplyToUserAsync(string.Format(ModuleTexts.SettingInThisModuleDoesNotExist, key, moduleName));
                return;
            }

            using var dbContext = _dbContextFactory.CreateDbContext();
            var setting = await dbContext.GuildsSettings.Get(Context.Guild.Id, key, moduleName);

            if (setting is null)
                setting = dbContext.GuildsSettings.Create(Context.Guild.Id, moduleName, key, value);
            else
                setting.Value = value;
            await dbContext.SaveChangesAsync();
            await ReplyToUserAsync(ModuleTexts.SettingSavedSuccessfully);
        }

        [RequireContext(ContextType.Guild)]
        [RequireUserPermission(GuildPermission.Administrator)]
        [Command("get")]
        [Priority(1)]
        public async Task Get(string moduleName, string key)
        {
            if (!Helpers.DoesSettingExists(_modulesHandler, key, moduleName))
            {
                await ReplyToUserAsync(string.Format(ModuleTexts.SettingInThisModuleDoesNotExist, key, moduleName));
                return;
            }

            using var dbContext = _dbContextFactory.CreateDbContext();
            var setting = await dbContext.GuildsSettings.Get(Context.Guild.Id, key, moduleName);

            string value = "-";
            if (setting is not null)
                value = setting.Value;

            await ReplyToUserAsync(string.Format(ModuleTexts.SettingGetInfo, key, moduleName, value));
        }

        [Command("help")]
        public async Task Help()
        {
            var modulesStr = Helpers.GetAllModulesNamesJoined(_modulesHandler);
            await ReplyToUserAsync(ModuleTexts.HelpTextMain + modulesStr);
        }

        [Command("help")]
        public async Task Help(string moduleName)
        {
            var moduleSettingsStr = Helpers.GetModuleSettingsJoined(_modulesHandler, moduleName);
            await ReplyToUserAsync(string.Format(ModuleTexts.HelpTextModule, moduleName) + moduleSettingsStr);
        }
    }
}
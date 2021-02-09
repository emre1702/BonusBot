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

namespace GuildSettingsModule
{
    [RequireContext(ContextType.Guild)]
    [RequireUserPermission(GuildPermission.Administrator)]
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

        [Command]
        public async Task Set(string moduleName, string key, [Remainder] string value)
        {
            if (!Helpers.DoesSettingExists(_modulesHandler, key, moduleName))
            {
                await ReplyToUserAsync(string.Format(ModuleTexts.SettingInThisModuleDoesNotExist, key, moduleName));
                return;
            }
            moduleName = moduleName.ToModuleName();

            using var dbContext = _dbContextFactory.CreateDbContext();
            var setting = await dbContext.GuildsSettings.FirstOrDefaultAsync(s =>
                s.GuildId == Context.Guild.Id &&
                EF.Functions.Like(s.Key, key) &&
                EF.Functions.Like(s.Module, moduleName));

            if (setting is null)
            {
                setting = new GuildsSettings { GuildId = Context.Guild.Id, Module = moduleName, Key = key };
                dbContext.GuildsSettings.Add(setting);
            }

            setting.Value = value;
            await dbContext.SaveChangesAsync();
            await ReplyToUserAsync(ModuleTexts.SettingSavedSuccessfully);
        }
    }
}
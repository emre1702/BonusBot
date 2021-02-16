using BonusBot.Common.Defaults;
using BonusBot.Common.Extensions;
using BonusBot.Database;
using BonusBot.GuildSettingsModule;
using BonusBot.GuildSettingsModule.Language;
using BonusBot.Services.DiscordNet;
using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace GuildSettingsModule
{
    [Group("config")]
    [Alias("settings", "setting")]
    public class GuildSettings : CommandBase
    {
        private readonly ModulesHandler _modulesHandler;
        private readonly BonusDbContextFactory _dbContextFactory;
        private static readonly SettingChangeEffects _settingChangeEffects = new();

        public GuildSettings(ModulesHandler modulesHandler, BonusDbContextFactory dbContextFactory)
        {
            _modulesHandler = modulesHandler;
            _dbContextFactory = dbContextFactory;

            ModuleTexts.Culture = Constants.Culture;
        }

        [RequireContext(ContextType.Guild)]
        [RequireUserPermission(GuildPermission.Administrator)]
        [Command]
        [Priority(0)]
        public async Task Set(string moduleName, string key, [Remainder] string value)
        {
            if (await SetSetting(moduleName, key, value))
                await ReplyToUserAsync(string.Format(ModuleTexts.SettingStringSavedSuccessfully, value));
        }

        [RequireContext(ContextType.Guild)]
        [RequireUserPermission(GuildPermission.Administrator)]
        [Command]
        [Priority(10)]
        public async Task Set(string moduleName, string key, IChannel channel)
        {
            if (await SetSetting(moduleName, key, channel.Id.ToString()))
                await ReplyToUserAsync(string.Format(ModuleTexts.SettingChannelSavedSuccessfully, channel.Name));
        }

        [RequireContext(ContextType.Guild)]
        [RequireUserPermission(GuildPermission.Administrator)]
        [Command]
        [Priority(10)]
        public async Task Set(string moduleName, string key, IRole role)
        {
            if (await SetSetting(moduleName, key, role.Id.ToString()))
                await ReplyToUserAsync(string.Format(ModuleTexts.SettingRoleSavedSuccessfully, role.Name));
        }

        [RequireContext(ContextType.Guild)]
        [RequireUserPermission(GuildPermission.Administrator)]
        [Command]
        [Priority(10)]
        public async Task Set(string moduleName, string key, IMessage message)
        {
            if (await SetSetting(moduleName, key, message.Id.ToString()))
                await ReplyToUserAsync(string.Format(ModuleTexts.SettingMessageSavedSuccessfully, message.Content));
        }

        [RequireContext(ContextType.Guild)]
        [RequireUserPermission(GuildPermission.Administrator)]
        [Command]
        [Priority(10)]
        public async Task Set(string moduleName, string key, IUser user)
        {
            if (await SetSetting(moduleName, key, user.Id.ToString()))
                await ReplyToUserAsync(string.Format(ModuleTexts.SettingUserSavedSuccessfully, user.Username + "#" + user.Discriminator));
        }

        private async Task<bool> SetSetting(string moduleName, string key, string value)
        {
            if (!Helpers.DoesSettingExists(_modulesHandler, key, moduleName))
            {
                await ReplyToUserAsync(string.Format(ModuleTexts.SettingInThisModuleDoesNotExist, key, moduleName));
                return false;
            }

            using var dbContext = _dbContextFactory.CreateDbContext();
            await dbContext.GuildsSettings.AddOrUpdate(Context.Guild.Id, key, moduleName, value);
            await dbContext.SaveChangesAsync();
            await _settingChangeEffects.Changed(Context.Guild, moduleName, key, value);
            return true;
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
            var setting = await GuildsSettingsExtensions.Get(dbContext.GuildsSettings, Context.Guild.Id, key, moduleName);

            string value = "-";
            if (setting is not null)
                value = setting.Value;

            await ReplyToUserAsync(string.Format(ModuleTexts.SettingGetInfo, key, moduleName, value));
        }

        [Command("help")]
        public async Task Help()
        {
            var modulesStr = Helpers.GetAllModulesAndCommonNamesJoined(_modulesHandler);
            await ReplyToUserAsync(string.Format(ModuleTexts.HelpTextMain, modulesStr));
        }

        [Command("help")]
        public async Task Help(string moduleName)
        {
            var moduleSettingsStr = Helpers.GetModuleSettingsJoined(_modulesHandler, moduleName);
            await ReplyToUserAsync(string.Format(ModuleTexts.HelpTextModule, moduleName, moduleSettingsStr));
        }
    }
}
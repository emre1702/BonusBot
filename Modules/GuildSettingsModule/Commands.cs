using BonusBot.Common.Defaults;
using BonusBot.Common.Extensions;
using BonusBot.Common.Interfaces.Services;
using BonusBot.GuildSettingsModule;
using BonusBot.GuildSettingsModule.Language;
using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace GuildSettingsModule
{
    [Group("config")]
    [Alias("settings", "setting")]
    public class GuildSettings : CommandBase
    {
        private readonly IModulesHandler _modulesHandler;
        private static readonly SettingChangeEffects _settingChangeEffects = new();
        private static readonly SettingValuePreparations _settingValuePreparations = new();

        public GuildSettings(IModulesHandler modulesHandler)
        {
            _modulesHandler = modulesHandler;
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
            if (await SetSetting(moduleName, key, channel))
                await ReplyToUserAsync(string.Format(ModuleTexts.SettingChannelSavedSuccessfully, channel.Name));
        }

        [RequireContext(ContextType.Guild)]
        [RequireUserPermission(GuildPermission.Administrator)]
        [Command]
        [Priority(10)]
        public async Task Set(string moduleName, string key, IRole role)
        {
            if (await SetSetting(moduleName, key, role))
                await ReplyToUserAsync(string.Format(ModuleTexts.SettingRoleSavedSuccessfully, role.Name));
        }

        [RequireContext(ContextType.Guild)]
        [RequireUserPermission(GuildPermission.Administrator)]
        [Command]
        [Priority(10)]
        public async Task Set(string moduleName, string key, IMessage message)
        {
            if (await SetSetting(moduleName, key, message))
                await ReplyToUserAsync(string.Format(ModuleTexts.SettingMessageSavedSuccessfully, message.Content));
        }

        [RequireContext(ContextType.Guild)]
        [RequireUserPermission(GuildPermission.Administrator)]
        [Command]
        [Priority(2)]
        public async Task Set(string moduleName, string key, IUser user)
        {
            if (await SetSetting(moduleName, key, user))
                await ReplyToUserAsync(string.Format(ModuleTexts.SettingUserSavedSuccessfully, user.Username + "#" + user.Discriminator));
        }

        private async Task<bool> SetSetting(string moduleName, string key, object value)
        {
            if (!Helpers.DoesSettingExists(_modulesHandler, key, moduleName))
            {
                await ReplyToUserAsync(string.Format(ModuleTexts.SettingInThisModuleDoesNotExist, key, moduleName));
                return false;
            }

            value = _settingValuePreparations.GetPreparedValue(moduleName, key, value);
            await Context.BonusGuild!.Settings.Set(moduleName, key, value);
            await _settingChangeEffects.Changed(Context.BonusGuild, moduleName, key, value);

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

            var value = await Context.BonusGuild!.Settings.Get<object>(moduleName, key) ?? "-";
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
using BonusBot.Common.Commands.Conditions;
using BonusBot.Common.Extensions;
using BonusBot.Common.Helper;
using BonusBot.GuildSettingsModule;
using BonusBot.GuildSettingsModule.Language;
using Discord;
using Discord.Commands;
using System.Linq;
using System.Threading.Tasks;

namespace GuildSettingsModule
{
    [RequireNotDisabledInGuild(typeof(GuildSettings))]
    public class GuildSettings : CommandBase
    {
        private static readonly SettingChangeEffects _settingChangeEffects = new();
        private static readonly SettingValuePreparations _settingValuePreparations = new();

        [RequireContext(ContextType.Guild)]
        [RequireUserPermission(GuildPermission.Administrator)]
        [Command("ConfigInteger")]
        [Alias("SettingsInteger", "SettingInteger")]
        public async Task Set(string moduleName, string key, int value)
        {
            if (await SetSetting(moduleName, key, value))
                await ReplyToUserAsync(string.Format(ModuleTexts.SettingNumberSavedSuccessfully, value));
        }

        [RequireContext(ContextType.Guild)]
        [RequireUserPermission(GuildPermission.Administrator)]
        [Command("ConfigString")]
        [Alias("SettingsString", "SettingString")]
        public async Task Set(string moduleName, string key, [Remainder] string value)
        {
            if (await SetSetting(moduleName, key, value))
                await ReplyToUserAsync(string.Format(ModuleTexts.SettingStringSavedSuccessfully, value));
        }

        [RequireContext(ContextType.Guild)]
        [RequireUserPermission(GuildPermission.Administrator)]
        [Command("ConfigBoolean")]
        [Alias("SettingsBoolean", "SettingBoolean")]
        public async Task Set(string moduleName, string key, bool value)
        {
            if (await SetSetting(moduleName, key, value))
                await ReplyToUserAsync(string.Format(ModuleTexts.SettingBoolSavedSuccessfully, value));
        }

        [RequireContext(ContextType.Guild)]
        [RequireUserPermission(GuildPermission.Administrator)]
        [Command("ConfigChannel")]
        [Alias("SettingsChannel", "SettingChannel")]
        public async Task Set(string moduleName, string key, IChannel channel)
        {
            if (await SetSetting(moduleName, key, channel))
                await ReplyToUserAsync(string.Format(ModuleTexts.SettingChannelSavedSuccessfully, channel.Name));
        }

        [RequireContext(ContextType.Guild)]
        [RequireUserPermission(GuildPermission.Administrator)]
        [Command("ConfigRole")]
        [Alias("SettingsRole", "SettingRole")]
        public async Task Set(string moduleName, string key, IRole role)
        {
            if (await SetSetting(moduleName, key, role))
                await ReplyToUserAsync(string.Format(ModuleTexts.SettingRoleSavedSuccessfully, role.Name));
        }

        /*[RequireContext(ContextType.Guild)]
        [RequireUserPermission(GuildPermission.Administrator)]
        [Command("ConfigEmote")]
        [Alias("SettingsEmote", "SettingEmote")]
        public async Task Set(string moduleName, string key, IEmote emote)
        {
            if (await SetSetting(moduleName, key, emote))
                await ReplyToUserAsync(string.Format(ModuleTexts.SettingEmoteSavedSuccessfully, emote.Name));
        }*/

        [RequireContext(ContextType.Guild)]
        [RequireUserPermission(GuildPermission.Administrator)]
        [Command("ConfigMessage")]
        [Alias("SettingsMessage", "SettingMessage")]
        public async Task Set(string moduleName, string key, IMessage message)
        {
            if (await SetSetting(moduleName, key, message))
                await ReplyToUserAsync(string.Format(ModuleTexts.SettingMessageSavedSuccessfully, message.Content));
        }

        [RequireContext(ContextType.Guild)]
        [RequireUserPermission(GuildPermission.Administrator)]
        [Command("ConfigUser")]
        [Alias("SettingsUser", "SettingUser")]
        public async Task Set(string moduleName, string key, IUser user)
        {
            if (await SetSetting(moduleName, key, user))
                await ReplyToUserAsync(string.Format(ModuleTexts.SettingUserSavedSuccessfully, user.Username + "#" + user.Discriminator));
        }

        private async Task<bool> SetSetting(string moduleName, string key, object value)
        {
            if (!SettingsHelper.DoesSettingExists(Context.BonusGuild!, key, moduleName))
            {
                await ReplyErrorToUserAsync(string.Format(ModuleTexts.SettingInThisModuleDoesNotExist, key, moduleName));
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
            if (!SettingsHelper.DoesSettingExists(Context.BonusGuild!, key, moduleName))
            {
                await ReplyErrorToUserAsync(string.Format(ModuleTexts.SettingInThisModuleDoesNotExist, key, moduleName));
                return;
            }

            var value = await Context.BonusGuild!.Settings.Get<object>(moduleName, key) ?? "-";
            await ReplyToUserAsync(string.Format(ModuleTexts.SettingGetInfo, key, moduleName, value));
        }

        [Command("help")]
        public async Task Help()
        {
            var modulesWithSettings = Context.BonusGuild!.Modules.GetActivatedModuleAssemblies().Where(SettingsHelper.HasSettings);
            var moduleNames = string.Join(", ", modulesWithSettings.Select(m => m.ToModuleName()));
            await ReplyToUserAsync(string.Format(ModuleTexts.HelpTextMain, "Common" + (moduleNames.Length > 0 ? (", " + moduleNames) : string.Empty)));
        }

        [Command("help")]
        public async Task Help(string moduleName)
        {
            var moduleSettingsStr = SettingsHelper.GetModuleSettingsJoined(Context.BonusGuild!, moduleName);
            await ReplyToUserAsync(string.Format(ModuleTexts.HelpTextModule, moduleName, moduleSettingsStr));
        }
    }
}
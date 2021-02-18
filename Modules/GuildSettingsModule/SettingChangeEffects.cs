using BonusBot.Common;
using BonusBot.Common.Extensions;
using BonusBot.Common.Interfaces.Guilds;
using BonusBot.GuildSettingsModule.Language;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace BonusBot.GuildSettingsModule
{
    internal class SettingChangeEffects
    {
        internal async ValueTask Changed(IBonusGuild bonusGuild, string moduleName, string key, object value)
        {
            if (moduleName.Equals(typeof(CommonSettings).GetModuleName(), StringComparison.CurrentCultureIgnoreCase))
                await CommonSettingChanged(bonusGuild, key, value);
        }

        private async ValueTask CommonSettingChanged(IBonusGuild bonusGuild, string key, object value)
        {
            if (key.Equals(CommonSettings.BotName, StringComparison.CurrentCultureIgnoreCase))
                await bonusGuild.DiscordGuild.CurrentUser.ModifyAsync(prop => prop.Nickname = value.ToString());
            else if (key.Equals(CommonSettings.Locale, StringComparison.CurrentCultureIgnoreCase))
            {
                bonusGuild.Settings.CultureInfo = (CultureInfo)value;
                ModuleTexts.Culture = bonusGuild.Settings.CultureInfo;
            }
        }
    }
}
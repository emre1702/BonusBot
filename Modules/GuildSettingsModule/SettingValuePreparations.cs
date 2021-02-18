using BonusBot.Common;
using BonusBot.Common.Extensions;
using System;
using System.Globalization;

namespace BonusBot.GuildSettingsModule
{
    internal class SettingValuePreparations
    {
        internal object GetPreparedValue(string moduleName, string key, object value)
        {
            if (moduleName.Equals(typeof(CommonSettings).GetModuleName(), StringComparison.CurrentCultureIgnoreCase))
                return GetPreparedCommonSettingValue(key, value);
            return value;
        }

        private object GetPreparedCommonSettingValue(string key, object value)
        {
            if (key.Equals(CommonSettings.Locale, StringComparison.CurrentCultureIgnoreCase))
                return CultureInfo.GetCultureInfo(value.ToString()!, false);
            return value;
        }
    }
}
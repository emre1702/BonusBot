using BonusBot.Common;
using BonusBot.Common.Extensions;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace BonusBot.GuildSettingsModule
{
    internal class SettingChangeEffects
    {
        internal async ValueTask Changed(SocketGuild guild, string moduleName, string key, string value)
        {
            if (moduleName.Equals(typeof(CommonSettings).GetModuleName(), StringComparison.CurrentCultureIgnoreCase))
                await CommonSettingChanged(guild, key, value);
        }

        private async ValueTask CommonSettingChanged(SocketGuild guild, string key, string value)
        {
            if (key.Equals(CommonSettings.BotName, StringComparison.CurrentCultureIgnoreCase))
                await guild.CurrentUser.ModifyAsync(prop => prop.Nickname = value);
        }
    }
}
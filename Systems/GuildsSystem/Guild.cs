using BonusBot.Common;
using BonusBot.Common.Defaults;
using BonusBot.Common.Interfaces.Guilds;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace BonusBot.GuildsSystem
{
    internal class Guild : IBonusGuild
    {
#nullable disable
        public SocketGuild DiscordGuild { get; private set; }
#nullable restore
        public IGuildModulesHandler Modules { get; }
        public IGuildSettingsHandler Settings { get; }

        public Guild(IGuildModulesHandler guildModulesHandler, IGuildSettingsHandler guildSettingsHandler)
        {
            Modules = guildModulesHandler;
            Settings = guildSettingsHandler;
        }

        public async Task Initialize(SocketGuild discordGuild)
        {
            DiscordGuild = discordGuild;
            await Settings.Init(discordGuild);
            await Modules.Init(Settings, discordGuild);

            var userName = await Settings.Get<string>(typeof(CommonSettings).Assembly, CommonSettings.BotName);

            if (DiscordGuild.CurrentUser.Nickname != userName)
            {
                await DiscordGuild.CurrentUser.ModifyAsync(prop =>
                {
                    prop.Nickname = userName ?? Constants.DefaultBotName;
                });
            }
        }
    }
}
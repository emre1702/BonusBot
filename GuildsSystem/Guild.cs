using BonusBot.Common;
using BonusBot.Common.Defaults;
using BonusBot.Common.Extensions;
using BonusBot.Common.Interfaces.Guilds;
using BonusBot.Database;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace GuildsSystem
{
    public class Guild : IBonusGuild
    {
        public SocketGuild DiscordGuild { get; }
        public IGuildSettingsHandler Settings { get; }

        private readonly BonusDbContextFactory _dbContextFactory;

        public Guild(SocketGuild discordGuild, BonusDbContextFactory dbContextFactory, IGuildSettingsHandler guildSettingsHandler)
        {
            DiscordGuild = discordGuild;
            _dbContextFactory = dbContextFactory;
            Settings = guildSettingsHandler;

            Settings.Init(discordGuild);
        }

        public async Task Initialize()
        {
            using var dbContext = _dbContextFactory.CreateDbContext();

            var userName = await dbContext.GuildsSettings.GetString(DiscordGuild.Id, CommonSettings.BotName, typeof(CommonSettings).Assembly);

            await DiscordGuild.CurrentUser.ModifyAsync(prop =>
            {
                prop.Nickname = userName ?? Constants.DefaultBotName;
            });
        }
    }
}
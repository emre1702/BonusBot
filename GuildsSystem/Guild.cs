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
        public IGuildSettingsHandler Settings { get; }

        private readonly SocketGuild _discordGuild;
        private readonly BonusDbContextFactory _dbContextFactory;

        public Guild(SocketGuild discordGuild, BonusDbContextFactory dbContextFactory, IGuildSettingsHandler guildSettingsHandler)
        {
            _discordGuild = discordGuild;
            _dbContextFactory = dbContextFactory;
            Settings = guildSettingsHandler;

            Settings.Init(discordGuild);
        }

        public async Task Initialize()
        {
            using var dbContext = _dbContextFactory.CreateDbContext();

            var userName = await dbContext.GuildsSettings.GetString(_discordGuild.Id, CommonSettings.BotName, typeof(CommonSettings).Assembly);

            await _discordGuild.CurrentUser.ModifyAsync(prop =>
            {
                prop.Nickname = userName ?? Constants.DefaultBotName;
            });
        }
    }
}
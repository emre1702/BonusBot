using BonusBot.Common;
using BonusBot.Common.Defaults;
using BonusBot.Common.Extensions;
using BonusBot.Common.Helper;
using BonusBot.Common.Interfaces.Guilds;
using BonusBot.Common.Interfaces.Services;
using BonusBot.Database;
using Discord.WebSocket;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;

namespace BonusBot.Services.Guilds
{
    public class GuildSettingsHandler : IGuildSettingsHandler
    {
#nullable disable
        public CultureInfo CultureInfo { get; set; }

        private SocketGuild _guild;
#nullable restore
        private readonly IGuildSettingsCache _settingsCache;
        private readonly BonusDbContextFactory _bonusDbContextFactory;
        private readonly IDiscordClientHandler _discordClientHandler;

        public GuildSettingsHandler(IGuildSettingsCache settingsCache, BonusDbContextFactory bonusDbContextFactory, IDiscordClientHandler discordClientHandler)
            => (_settingsCache, _bonusDbContextFactory, _discordClientHandler) = (settingsCache, bonusDbContextFactory, discordClientHandler);

        public async Task Init(SocketGuild guild)
        {
            _guild = guild;
            CultureInfo = await Get<CultureInfo>(typeof(CommonSettings).Assembly, CommonSettings.Locale) ?? Constants.DefaultCultureInfo;
        }

        public async ValueTask<T?> Get<T>(string moduleName, string key)
        {
            moduleName = moduleName.ToModuleName();
            var client = await _discordClientHandler.ClientSource.Task;

            var cachedSetting = _settingsCache.Get(moduleName, key);
            if (cachedSetting is { })
                return await cachedSetting.ConvertTo<T>(client, _guild);

            using var dbContext = _bonusDbContextFactory.CreateDbContext();
            var setting = await dbContext.GuildsSettings.Get(_guild.Id, key, moduleName);
            if (setting is null)
                return default;

            return await setting.Value.ConvertTo<T>(client, _guild);
        }

        public ValueTask<T?> Get<T>(Assembly moduleAssembly, string key)
            => Get<T>(moduleAssembly.ToModuleName(), key);

        public async Task Set(string moduleName, string key, object value)
        {
            moduleName = moduleName.ToModuleName();
            _settingsCache.Set(moduleName, key, value);

            var identifier = value.GetIdentifier();
            using var dbContext = _bonusDbContextFactory.CreateDbContext();
            await dbContext.GuildsSettings.AddOrUpdate(_guild.Id, key, moduleName, identifier);
            await dbContext.SaveChangesAsync();
        }

        public Task Set(Assembly moduleAssembly, string key, object value)
            => Set(moduleAssembly.ToModuleName(), key, value);
    }
}
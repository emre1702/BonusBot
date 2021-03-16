using BonusBot.Common.Extensions;
using BonusBot.Common.Interfaces.Guilds;
using BonusBot.Common.Interfaces.Services;
using BonusBot.WebDashboardBoardModule.Enums.Content;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BonusBot.WebDashboardBoardModule.Services
{
    public class ContentService
    {
        private readonly IModulesHandler _modulesHandler;
        private readonly IGuildsHandler _guildsHandler;

        public ContentService(IModulesHandler modulesHandler, IGuildsHandler guildsHandler)
            => (_modulesHandler, _guildsHandler) = (modulesHandler, guildsHandler);

        public async Task<UserAccessLevel> GetUserAccessLevel(string module, string guildId)
        {
            module = module.ToModuleName();
            if (await IsDisabledInBot(module))
                return UserAccessLevel.DisabledInBot;
            if (IsDisabledInGuild(module, guildId))
                return UserAccessLevel.DisabledInGuild;

            return UserAccessLevel.HasAccess;
        }

        private async Task<bool> IsDisabledInBot(string module)
        {
            await _modulesHandler.LoadModulesTaskSource.Task;

            return !_modulesHandler.LoadedModuleAssemblies.Any(a => a.ToModuleName().Equals(module, System.StringComparison.OrdinalIgnoreCase));
        }

        private bool IsDisabledInGuild(string module, string guildId)
        {
            var guild = _guildsHandler.GetGuild(ulong.Parse(guildId));
            if (guild is null)
                throw new InvalidOperationException("Bot is not in guild!");

            if (guild.Modules.GetActivatedModuleAssembly(module) is null)
                return true;
            return false;
        }
    }
}

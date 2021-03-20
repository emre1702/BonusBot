using BonusBot.Common.Interfaces.Services;
using BonusBot.WebDashboardModule.Defaults;
using BonusBot.WebDashboardModule.Extensions;
using BonusBot.WebDashboardModule.Models.Discord;
using BonusBot.WebDashboardModule.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BonusBot.WebDashboardModule.Controllers.Navigation
{
    [Route("[controller]")]
    public class NavigationController : Controller
    {
        private readonly NavigationService _navigationService;

        public NavigationController(IDiscordClientHandler discordClientHandler)
            => _navigationService = new(discordClientHandler);

        [HttpGet("Guilds")]
        public async Task<ActionResult<IEnumerable<WebGuildData>>> GetGuilds()
        {
            var tokenData = HttpContext.Session.Get<TokenData>(SessionKeys.TokenData);
            if (tokenData is null) return new UnauthorizedResult();

            var guilds = (await _navigationService.GetGuilds(tokenData)).ToList();
            HttpContext.Session.Set(SessionKeys.UserGuildIds, guilds.Select(g => g.Id));

            return Ok(guilds);
        }
    }
}

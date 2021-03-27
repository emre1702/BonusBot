using BonusBot.Common.Interfaces.Guilds;
using BonusBot.WebDashboardModule.Defaults;
using BonusBot.WebDashboardModule.Extensions;
using BonusBot.WebDashboardModule.Models.Color;
using BonusBot.WebDashboardModule.Models.Discord;
using BonusBot.WebDashboardModule.Services;
using Discord.WebSocket;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using DiscordColor = Discord.Color;

namespace BonusBot.WebDashboardModule.Controllers.Color
{
    [Route("[controller]")]
    public class ColorController : Controller
    {
        private readonly UserValidationService _userValidationService = new();
        private readonly IGuildsHandler _guildsHandler;

        public ColorController(IGuildsHandler guildsHandler)
            => _guildsHandler = guildsHandler;

        [HttpGet("UserColor")]
        public ActionResult<DiscordColor> GetUserColor(string guildId)
        {
            _userValidationService.AssertIsInGuild(HttpContext.Session, guildId);

            var bonusGuild = _guildsHandler.GetGuild(ulong.Parse(guildId));
            if (bonusGuild is null) return DiscordColor.Default;

            var user = GetUser(bonusGuild.DiscordGuild);
            return GetUserColor(user);
        }

        private SocketGuildUser GetUser(SocketGuild guild)
        {
            var userData = HttpContext.Session.Get<UserResponseData>(SessionKeys.UserData)!;
            return guild.GetUser(userData.Id);
        }

        private DiscordColor GetUserColor(SocketGuildUser user)
            => user.Roles.FirstOrDefault(r => r.Name.StartsWith("Role "))?.Color ?? DiscordColor.Default;
    }
}

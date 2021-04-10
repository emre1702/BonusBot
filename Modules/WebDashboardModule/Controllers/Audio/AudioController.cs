using BonusBot.AudioModule.Helpers;
using BonusBot.AudioModule.LavaLink.Enums;
using BonusBot.Common.Interfaces.Guilds;
using BonusBot.WebDashboardModule.Models.Audio;
using BonusBot.WebDashboardModule.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BonusBot.WebDashboardModule.Controllers.Audio
{
    [Route("[controller]")]
    public class AudioController : Controller
    {
        private readonly UserValidationService _userValidationService;
        private readonly IGuildsHandler _guildsHandler;

        public AudioController(UserValidationService userValidationService, IGuildsHandler guildsHandler)
            => (_userValidationService, _guildsHandler) = (userValidationService, guildsHandler);

        [HttpGet("GetState")]
        public async Task<ActionResult<AudioSettingsState>> GetState([FromQuery] string guildId)
        {
            _userValidationService.AssertIsInGuild(HttpContext.Session, guildId);

            var bonusGuild = _guildsHandler.GetGuild(ulong.Parse(guildId));

            var volume = (await VolumeHelper.GetVolume(bonusGuild!)) ?? 100;
            var status = WebStateHelper.GetPlayerStatus(bonusGuild!.DiscordGuild.Id) ?? PlayerStatus.Disconnected;

            var state = new AudioSettingsState(volume, status);
            return Ok(state);
        }
    }
}

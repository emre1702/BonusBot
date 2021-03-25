using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BonusBot.WebDashboardModule.Services
{
    public class SettingsService
    {
        /*public async Task<IActionResult> GetAudioSettingsState([FromQuery] string guildId)
        {
            _userValidationService.AssertIsInGuild(HttpContext.Session, guildId);

            var webCommandService = new WebCommandService(_guildsHandler, _discordClientHandler, _commandsHandler, _mainServiceProvider);
            var messages = await webCommandService.Execute(HttpContext.Session, guildId, "GetState");

            var json = messages.FirstOrDefault(m => m.StartsWith("{"));
            if (json is not null)
                return Ok(json);
            return Ok("{}");
        }*/
    }
}

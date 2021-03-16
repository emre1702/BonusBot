using BonusBot.WebDashboardBoardModule.Defaults;
using Microsoft.AspNetCore.Mvc;
using System;
using BonusBot.WebDashboardBoardModule.Extensions;

namespace BonusBot.WebDashboardBoardModule.Controllers.Account
{
    [Route("[controller]")]
    public class LoginController : Controller
    {
        [HttpGet("OAuthUrl")]
        public ActionResult<string> GetOAuthUrl()
        {
            var baseUrl = WebConstants.OAuthUrl;
            var clientId = Environment.GetEnvironmentVariable(WebEnvironmentKeys.BotClientId);
            var state = SetAndReturnState();
            return Ok(string.Format(baseUrl, clientId, state));
        }

        private string SetAndReturnState()
        {
            var guid = new Guid().ToString();
            HttpContext.Session.Set(SessionKeys.TokenState, guid);
            return guid;
        }
    }
}

using BonusBot.WebDashboardBoardModule.Defaults;
using Microsoft.AspNetCore.Mvc;
using System;
using BonusBot.WebDashboardBoardModule.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace BonusBot.WebDashboardBoardModule.Controllers.Account
{
    [Route("[controller]")]
    [AllowAnonymous]
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

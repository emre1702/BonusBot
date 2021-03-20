using BonusBot.WebDashboardModule.Defaults;
using Microsoft.AspNetCore.Mvc;
using System;
using BonusBot.WebDashboardModule.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace BonusBot.WebDashboardModule.Controllers.Account
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
            var guid = Guid.NewGuid().ToString();
            HttpContext.Session.Set(SessionKeys.TokenState, guid);
            return guid;
        }
    }
}

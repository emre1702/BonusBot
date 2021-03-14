using BonusBot.WebDashboardBoardModule.Defaults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BonusBot.WebDashboardBoardModule.Controllers.Account
{
    [Route("[controller]")]
    public class LoginController : Controller
    {

        [HttpGet("OAuthUrl")]
        [AllowAnonymous]
        public ActionResult<string> GetOAuthUrl()
        {
            var baseUrl = WebConstants.OAuthUrl;
            var clientId = Environment.GetEnvironmentVariable(WebEnvironmentKeys.BotClientId);
            return Ok(string.Format(baseUrl, clientId));
        }
    }
}

using BonusBot.WebDashboardModule.Defaults;
using Microsoft.AspNetCore.Mvc;
using System;
using BonusBot.WebDashboardModule.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace BonusBot.WebDashboardModule.Controllers.Account
{
    [Route("[controller]")]
    [AllowAnonymous]
    public class LoginController : Controller
    {
        [HttpGet("OAuthUrl")]
        public ActionResult<string> GetOAuthUrl()
        {
            if (HasValidSession(HttpContext.Session)) return Redirect("/");

            var baseUrl = WebConstants.OAuthUrl;
            var clientId = Environment.GetEnvironmentVariable(WebEnvironmentKeys.BotClientId);
            var redirectUrl = string.Format(WebConstants.OAuthTokenUrlRedirectUri, Environment.GetEnvironmentVariable(WebEnvironmentKeys.WebBaseUrl)!);
            var state = SetAndReturnState();
            return Ok(string.Format(baseUrl, clientId, Uri.EscapeUriString(redirectUrl), state));
        }

        private string SetAndReturnState()
        {
            var guid = Guid.NewGuid().ToString();
            HttpContext.Session.Set(SessionKeys.TokenState, guid);
            return guid;
        }

        private bool HasValidSession(ISession session)
        {
            long? expireUnixSeconds = session.Get<long>(SessionKeys.ExpireUnixSeconds);
            return expireUnixSeconds is not null && expireUnixSeconds > DateTime.UtcNow.ToUnixTimeSeconds();
        }
    }
}

using BonusBot.WebDashboardBoardModule.Defaults;
using BonusBot.WebDashboardBoardModule.Extensions;
using BonusBot.WebDashboardBoardModule.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BonusBot.WebDashboardBoardModule.Controllers.Account
{
    [Route("[controller]")]
    public class LoginRedirectController : Controller
    {
        private static readonly TokenRequestService _tokenRequestService = new();
        private static readonly UserRequestService _userRequestService = new();

        public async Task<ActionResult> Index([FromQuery] string code, [FromQuery] string state)
        {
            if (code is null) return Redirect("/");

            var currentState = HttpContext.Session.Get<string>(SessionKeys.TokenState);
            if (currentState is null || currentState != state) throw new InvalidOperationException("Please try again!");

            var tokenData = await _tokenRequestService.GetTokenData(code);
            var userData = await _userRequestService.GetUser(tokenData);

            HttpContext.Session.Set(SessionKeys.ExpireUnixSeconds, DateTime.UtcNow.ToUnixTimeSeconds(tokenData.ExpiresInSeconds));
            HttpContext.Session.Set(SessionKeys.UserData, userData);
            HttpContext.Session.Set(SessionKeys.TokenData, tokenData);
            HttpContext.Session.Remove(SessionKeys.TokenState);
            await HttpContext.Session.CommitAsync();

            return Redirect("/");
        }
    }
}

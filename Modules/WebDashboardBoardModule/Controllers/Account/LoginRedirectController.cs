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

        [AllowAnonymous]
        public async Task<ActionResult> Index([FromQuery] string code)
        {
            if (code is null) return Redirect("/");
           
            var tokenData = await _tokenRequestService.GetTokenData(code);
            var userData = await _userRequestService.GetUser(tokenData);

            HttpContext.Session.Set(SessionKeys.ExpireUnixSeconds, DateTime.UtcNow.ToUnixTimeSeconds(tokenData.ExpiresInSeconds));
            HttpContext.Session.Set(SessionKeys.UserData, userData);
            HttpContext.Session.Set(SessionKeys.TokenData, tokenData);
            await HttpContext.Session.CommitAsync();

            return Redirect("/");
        }

        [AllowAnonymous]
        [Route("Redirect")]
        public ActionResult Index()
        {
            return Ok();
        }
    }
}

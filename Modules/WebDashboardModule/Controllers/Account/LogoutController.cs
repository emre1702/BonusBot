using BonusBot.WebDashboardModule.Defaults;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BonusBot.WebDashboardModule.Controllers.Account
{
    [Route("[controller]")]
    public class LogoutController : Controller
    {
        [HttpPost("Execute")]
        public async Task<IActionResult> Execute()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.Session.Remove(SessionKeys.TokenData);
            HttpContext.Session.Remove(SessionKeys.TokenState);
            HttpContext.Session.Remove(SessionKeys.ExpireUnixSeconds);
            HttpContext.Session.Remove(SessionKeys.UserData);
            await HttpContext.Session.CommitAsync();

            return Ok();
        }
    }
}

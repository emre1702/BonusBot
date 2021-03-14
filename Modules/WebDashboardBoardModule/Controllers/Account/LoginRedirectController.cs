using BonusBot.WebDashboardBoardModule.Defaults;
using BonusBot.WebDashboardBoardModule.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BonusBot.WebDashboardBoardModule.Controllers.Account
{
    [Route("[controller]")]
    public class LoginRedirectController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index([FromQuery] string code)
        {
            HttpContext.Session.Set(SessionKeys.Token, code);
            HttpContext.Session.CommitAsync();
            return Redirect("/");
        }
    }
}

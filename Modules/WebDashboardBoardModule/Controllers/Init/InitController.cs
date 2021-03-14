using Microsoft.AspNetCore.Mvc;

namespace BonusBot.WebDashboardBoardModule.Controllers.Navigation
{
    [Route("[controller]")]
    public class InitController : Controller
    {
        [HttpGet("Init")]
        public IActionResult Init() => Ok();
    }
}

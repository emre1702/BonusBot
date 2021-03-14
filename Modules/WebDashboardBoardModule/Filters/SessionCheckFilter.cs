using BonusBot.WebDashboardBoardModule.Defaults;
using BonusBot.WebDashboardBoardModule.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BonusBot.WebDashboardBoardModule.Filters
{
    public class SessionCheckFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!IsInLogin(context) && context.HttpContext.Session.Get<string>(SessionKeys.Token) is null)
                context.Result = new UnauthorizedResult();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        private bool IsInLogin(ActionExecutingContext context)
            => context.HttpContext.Request.Path.Value?.StartsWith("/Login") == true;
    }
}

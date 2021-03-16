using BonusBot.WebDashboardBoardModule.Defaults;
using BonusBot.WebDashboardBoardModule.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace BonusBot.WebDashboardBoardModule.Filters
{
    public class SessionCheckFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!IsInLogin(context) && !HasValidSession(context.HttpContext.Session))
                context.Result = new UnauthorizedResult();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        private bool IsInLogin(ActionExecutingContext context)
            => context.HttpContext.Request.Path.Value?.StartsWith("/Login", StringComparison.OrdinalIgnoreCase) == true;

        private bool HasValidSession(ISession session)
        {
            if (!session.Has(SessionKeys.UserData))
                return false;
            var sessionExpireSec = session.Get<long>(SessionKeys.ExpireUnixSeconds);
            if (sessionExpireSec <= DateTime.UtcNow.ToUnixTimeSeconds())
            {
                //Todo: If token expired, request new with tokendata 
                session.Remove(SessionKeys.UserData);
                session.Remove(SessionKeys.TokenData);
                session.Remove(SessionKeys.UserGuildIds);
                session.CommitAsync();
                return false;
            }

            return true;
        }
    }
}

using BonusBot.WebDashboardBoardModule.Defaults;
using BonusBot.WebDashboardBoardModule.Extensions;
using BonusBot.WebDashboardBoardModule.Models.Discord;
using BonusBot.WebDashboardBoardModule.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BonusBot.WebDashboardBoardModule.Controllers.Account
{
    [Route("[controller]")]
    [AllowAnonymous]
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
            var expiresIn = DateTimeOffset.UtcNow.AddSeconds(tokenData.ExpiresInSeconds);

            await SetSessionData(tokenData, userData, expiresIn);
            await SignIn(expiresIn);

            return Redirect("/");
        }

        private async Task SetSessionData(TokenData tokenData, UserResponseData userData, DateTimeOffset expires)
        {
            HttpContext.Session.Set(SessionKeys.ExpireUnixSeconds, expires.ToUnixTimeSeconds());
            HttpContext.Session.Set(SessionKeys.UserData, userData);
            HttpContext.Session.Set(SessionKeys.TokenData, tokenData);
            HttpContext.Session.Remove(SessionKeys.TokenState);
            await HttpContext.Session.CommitAsync();
        }

        private async Task SignIn(DateTimeOffset expires)
        {
            var claims = new List<Claim>();
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = expires,
                IsPersistent = true
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
        }
    }
}

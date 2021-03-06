﻿using BonusBot.WebDashboardModule.Defaults;
using BonusBot.WebDashboardModule.Extensions;
using BonusBot.WebDashboardModule.Models.Discord;
using BonusBot.WebDashboardModule.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BonusBot.WebDashboardModule.Controllers.Account
{
    [Route("[controller]")]
    [AllowAnonymous]
    public class LoginRedirectController : Controller
    {
        private readonly TokenRequestService _tokenRequestService;
        private readonly UserRequestService _userRequestService;

        public LoginRedirectController(TokenRequestService tokenRequestService, UserRequestService userRequestService)
            => (_tokenRequestService, _userRequestService) = (tokenRequestService, userRequestService);

        public async Task<ActionResult> Index([FromQuery] string code, [FromQuery] string state)
        {
            if (code is null) return Redirect("/");

            var currentState = HttpContext.Session.Get<string>(SessionKeys.TokenState);
            if (currentState is null || currentState != state) return Redirect("/login");

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

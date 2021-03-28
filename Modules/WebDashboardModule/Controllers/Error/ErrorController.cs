﻿using BonusBot.WebDashboardModule.Pages;
using Microsoft.AspNetCore.Mvc;

namespace BonusBot.WebDashboardModule.Controllers.Error
{
    [Route("[controller]")]
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View(new ErrorModel());
        }
    }
}

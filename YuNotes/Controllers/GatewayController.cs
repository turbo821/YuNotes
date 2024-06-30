﻿using Microsoft.AspNetCore.Mvc;

namespace YuNotes.Controllers
{
    public class GatewayController : Controller
    {
        [Route("/login")]
        public IActionResult Login()
        {
            return View();
        }

        [Route("/registration")]
        public IActionResult Registration()
        {
            return View();
        }

        [Route("/code")]
        public IActionResult Code()
        {
            return View();
        }
    }
}

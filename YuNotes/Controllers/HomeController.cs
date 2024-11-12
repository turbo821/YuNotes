using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using YuNotes.Models;

namespace YuNotes.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }
    }
}

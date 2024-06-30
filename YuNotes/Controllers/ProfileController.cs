using Microsoft.AspNetCore.Mvc;

namespace YuNotes.Controllers
{
    public class ProfileController : Controller
    {
        [Route("/catalog")]
        public IActionResult Catalog()
        {
            return View();
        }

        [Route("/note")]
        public IActionResult Note()
        {
            return View();
        }

        [Route("/info")]
        public IActionResult Info()
        {
            return View();
        }
    }
}

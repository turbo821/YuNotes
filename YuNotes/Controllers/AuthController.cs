using Microsoft.AspNetCore.Mvc;

namespace YuNotes.Controllers
{
    public class AuthController : Controller
    {
        [Route("/login")]
        public IActionResult Login()
        {
            return View();
        }

        [Route("/signup")]
        public IActionResult SignUp()
        {
            return View();
        }

        [Route("/inputemail")]
        public IActionResult InputEmail()
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

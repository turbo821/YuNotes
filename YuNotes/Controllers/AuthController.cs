using Microsoft.AspNetCore.Mvc;
using YuNotes.ViewModels;

namespace YuNotes.Controllers
{
    public class AuthController : Controller
    {
        [HttpGet]
        [Route("/login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> LoginAsync(LogInViewModel request)
        {
            Console.WriteLine($"{request.Email} -- {request.Password}");
            return RedirectToAction("Catalog", "Notes");
        }

        [HttpGet]
        [Route("/signup")]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [Route("/signup")]
        public async Task<IActionResult> SignUpAsync(SignInViewModel model)
        {
            Console.WriteLine("Hello");
            return RedirectToAction("Login");
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

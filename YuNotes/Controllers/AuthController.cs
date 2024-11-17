using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using YuNotes.ViewModels;
using YuNotes.Services.Interfaces;

namespace YuNotes.Controllers
{
    public class AuthController : Controller
    {
        private IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("/login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> LoginAsync(string? returnUrl, LogInViewModel request)
        {
            if (await _service.CheckLogin(request))
            {
                ClaimsIdentity claimsIdentity = _service.GetClaimsIdentity(request);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return Redirect(returnUrl ?? "/");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpGet]
        [Route("/logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
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
            if (_service.NicknameIsRetryOrNull(model.Nickname))
            {
                return RedirectToAction("SignUp");
            }
            else
            {
                await _service.SignUpUser(model);
            }
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

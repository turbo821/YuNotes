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
        public IActionResult Login(string? returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
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
                ModelState.AddModelError("", "Почта или пароль неверны!");
                return View(request);
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
                ModelState.AddModelError("Nickname", "Аккаунт с таким ником уже создан");
            }
            else if(_service.EmailIsRetryOrNull(model.Email))
            {
                ModelState.AddModelError("Email", "Аккаунт с такой почтой уже создан");
            }
            else if(ModelState.IsValid)
            {
                await _service.SignUpUser(model);
                return RedirectToAction("Login");
            }
            return View(model);
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

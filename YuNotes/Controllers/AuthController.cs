using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using YuNotes.ViewModels;
using YuNotes.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace YuNotes.Controllers
{
    public class AuthController : Controller
    {
        private IAuthService _authService;
        private IPasswordRecoveryService _recoveryService;

        public AuthController(IAuthService authService, IPasswordRecoveryService recoveryService)
        {
            _authService = authService;
            _recoveryService = recoveryService;
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
            if (ModelState.IsValid && await _authService.CheckLogin(request.Email, request.Password))
            {
                ClaimsIdentity claimsIdentity = _authService.GetClaimsIdentity(request);

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
            if (_authService.NicknameIsRetryOrNull(model.Nickname))
            {
                ModelState.AddModelError("Nickname", "Аккаунт с таким ником уже создан");
            }
            else if(_authService.EmailIsRetryOrNull(model.Email))
            {
                ModelState.AddModelError("Email", "Аккаунт с такой почтой уже создан");
            }
            else if(ModelState.IsValid)
            {
                await _authService.SignUpUser(model);
                return RedirectToAction("Login");
            }
            return View(model);
        }

        [HttpGet]
        [Route("/inputemail")]
        public IActionResult InputEmail(EmailViewModel? model)
        {
            if(model is not null)
            {
                model.Email = string.Empty;
            }
            else
            {
                model = new EmailViewModel() { Email = string.Empty };
            }
            return View(model);
        }

        [HttpPost]
        [Route("/code")]
        public async Task<IActionResult> Code(EmailViewModel model)
        {
            if (ModelState.IsValid && !string.IsNullOrEmpty(model.Email) && _authService.EmailIsRetryOrNull(model.Email!))
            {
                string code = await _recoveryService.SendCodeAsync(model.Email!);
                PasswordRecoveryViewModel recovery = new PasswordRecoveryViewModel() { Code = code, Email = model.Email! };
                return View(recovery);
            }
            else
            {
                ModelState.AddModelError("", "Профиля с таким email не существует!");
                return View(nameof(InputEmail), model);
            }

        }

        [HttpPost]
        [Route("/recovery")]
        public async Task<IActionResult> Recovery(PasswordRecoveryViewModel model)
        {
            if (ModelState.IsValid && model.Code == model.InputCode && _authService.EmailIsRetryOrNull(model.Email))
            {
                await _authService.UpdatePassword(model.Email, model.Password);
                return RedirectToAction("Login");
            }
            else
            {
                ModelState.AddModelError("", "Код неверный, повторите попытку!");
                return View(nameof(InputEmail), new EmailViewModel() { Email = model.Email });
            }
            
        }
    }
}

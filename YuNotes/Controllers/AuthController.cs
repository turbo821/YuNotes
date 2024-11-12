using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using YuNotes.Auth;
using YuNotes.Models;
using YuNotes.Repositories;
using YuNotes.ViewModels;

namespace YuNotes.Controllers
{
    public class AuthController : Controller
    {
        IUsersReposiroty repo;
        public AuthController(IUsersReposiroty repo)
        {
            this.repo = repo;
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
            if (request.Email != null && request.Password != null && await repo.LoginUser(request.Email, request.Password))
            {
                var claims = new List<Claim> { new Claim(ClaimTypes.Email, request.Email) };
                
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                
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
            if (repo.CheckNickname(model.Nickname) || model.Nickname == null)
            {
                return RedirectToAction("SignUp");
            }
            else
            {
                User user = new User { Nickname = model.Nickname!, Email = model.Email, Password = model.Password.Encrypt() };
                await repo.SignUpUser(user);
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

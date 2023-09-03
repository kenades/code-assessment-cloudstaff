using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ContactsCS.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace ContactsCS.Controllers
{
    public class LoginController : Controller
    {
        [AllowAnonymous]
        public IActionResult Login()
        {
            ClaimsPrincipal claims = HttpContext.User;

            if (claims.Identity.IsAuthenticated)
            {
                return View("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Credentials credential)
        {

            if (credential.UserName == "cloudstaff"
                && credential.Password == "cloudstaff")
            {
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, credential.UserName));
                claims.Add(new Claim("OtherProperty", "Sample Role"));

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = credential.KeepSignedIn
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);

                return RedirectToAction("Index", "Home");
            }

            ViewData["AuthMessage"] = "Invalid credentials. Please try again.";
            return View();
        }
    }
}

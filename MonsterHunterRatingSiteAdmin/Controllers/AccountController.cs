using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MonsterHunterRatingSiteAdmin.Controllers
{
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public async Task Login(string returnUrl = "/")
        {
            // Only redirect back inside this app after login
            if (!Url.IsLocalUrl(returnUrl))
            {
                returnUrl = "/";
            }

            var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
                .WithRedirectUri(returnUrl)
                .Build();

            await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
        }

        [Authorize]
        public async Task Logout()
        {
            var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
                .WithRedirectUri(Url.Action("Index", "Home")!)
                .Build();

            await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        // Shows the logged-in user's claims, handy for checking roles came through
        [Authorize]
        public IActionResult Profile()
        {
            return View();
        }

        // Cookie handler sends logged-in users without the right role here
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}

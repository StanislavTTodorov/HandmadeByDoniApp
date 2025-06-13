
//using HandmadeByDoniApp.Web.Resources;

using static HandmadeByDoniApp.Common.NotificationMessagesConstants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.Extensions.Localization;
using Resources.Resources;
using HandmadeByDoniApp.Data.Models;

namespace HandmadeByDoniApp.Web.Controllers
{
    public class GoogleController : BaseController<GoogleController>
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IStringLocalizer<App> L;

        public GoogleController(SignInManager<ApplicationUser> signInManager,
                                 UserManager<ApplicationUser> userManager,
                                 IStringLocalizer<App> L)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.L = L;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Google(string? returnUrl = null)
        {
            string redirectUrl = Url.Action(nameof(GoogleResponse), "Google", new { returnUrl });
            var properties = signInManager.ConfigureExternalAuthenticationProperties(GoogleDefaults.AuthenticationScheme, redirectUrl);
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GoogleResponse(string? returnUrl = null)
        {
            var info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                this.TempData[ErrorMessage] = L["UnexpectedError"].Value;
                return RedirectToAction("Login", "User");
            }

            var signInResult = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
            if (signInResult.Succeeded)
            {
                return LocalRedirect(returnUrl ?? "/");
            }

            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            if (email == null)
            {
                this.TempData[ErrorMessage] = L["UnexpectedError"].Value;
                return RedirectToAction("Login", "User");
            }

            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    Email = email,
                    UserName = email,
                    FirstName = info.Principal.FindFirstValue(ClaimTypes.GivenName) ?? string.Empty,
                    LastName = info.Principal.FindFirstValue(ClaimTypes.Surname) ?? string.Empty
                };

                var result = await userManager.CreateAsync(user);
                if (!result.Succeeded)
                {
                    this.TempData[ErrorMessage] = L["UnexpectedError"].Value;
                    return RedirectToAction("Login", "User");
                }

                await userManager.AddLoginAsync(user, info);
            }

            await signInManager.SignInAsync(user, isPersistent: false);
            return LocalRedirect(returnUrl ?? "/");
        }
    }
}


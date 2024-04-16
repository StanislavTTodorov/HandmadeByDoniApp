﻿
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Griesoft.AspNetCore.ReCaptcha;
using HandmadeByDoniApp.Data.Models;
using Microsoft.Extensions.Caching.Memory;
using HandmadeByDoniApp.Web.ViewModels.User;

using static HandmadeByDoniApp.Common.GeneralMessages;
using static HandmadeByDoniApp.Common.NotificationMessagesConstants;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

namespace HandmadeByDoniApp.Web.Controllers
{
    

    public class UserController : BaseController
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMemoryCache memoryCache;

        public UserController(SignInManager<ApplicationUser> signInManager,
                              UserManager<ApplicationUser> userManager,
                              IMemoryCache memoryCache)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;

            this.memoryCache = memoryCache;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateRecaptcha(Action = nameof(Register),
        ValidationFailedAction = ValidationFailedAction.ContinueRequest)]
        public async Task<IActionResult> Register(RegisterFormModel model)
        {
            if (ModelState.IsValid == false)
            { 
                model.LastName = string.Empty;
                return this.View(model);
            }

            ApplicationUser user = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            // await this.userManager.AddClaimAsync(user, new Claim("FirstName", model.FirstName));
            // await this.userManager.AddClaimAsync(user, new Claim("LastName", model.LastName));

            await this.userManager.SetEmailAsync(user, model.Email);
            await this.userManager.SetUserNameAsync(user, model.Email);

            IdentityResult result = 
                await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded==false)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return this.View(model);
            }

            await signInManager.SignInAsync(user, false);
            //this.memoryCache.Remove(UsersCacheKey);

            return this.RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string? returnUrl = null)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            LoginFormModel model = new LoginFormModel()
            {
                ReturnUrl = returnUrl
            };

            return this.View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginFormModel model)
        {
            if (ModelState.IsValid==false)
            {
                return this.View(model);
            }

            var result = 
                await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

            if (result.Succeeded==false)
            {
                TempData[ErrorMessage] = LogginError;
                return this.View(model);
            }

            return this.Redirect(model.ReturnUrl ?? "/Home/Index");
        }
    }
}

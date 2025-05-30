
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
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace HandmadeByDoniApp.Web.Controllers
{


    public class UserController : BaseController<UserController>
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMemoryCache memoryCache;
        private readonly ILogger<UserController> logger;

        public UserController(SignInManager<ApplicationUser> signInManager,
                              UserManager<ApplicationUser> userManager,
                              IMemoryCache memoryCache,
                              ILogger<UserController> logger)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;

            this.memoryCache = memoryCache;
            this.logger = logger;
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

            if (result.Succeeded == false)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return this.View(model);
            }

            //За сега ще е така при Google login TODO
            string token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            result = await userManager.ConfirmEmailAsync(user, token);

            await signInManager.SignInAsync(user, false);
            //this.memoryCache.Remove(UsersCacheKey);

            return this.RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string? returnUrl = null)
        {
            logger.LogWarning("Login page accessed.");
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            LoginFormModel model = new LoginFormModel()
            {
                ReturnUrl = returnUrl
            };
            logger.LogWarning("Login page model created.");
            return this.View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginFormModel model)
        {
            logger.LogWarning("Login post action accessed.");
            if (ModelState.IsValid == false)
            {
                logger.LogCritical("Model state is not valid.");
                return this.View(model);
            }
            logger.LogWarning("Model state is valid.");
            var result =
                await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            logger.LogWarning("Password sign in result: {result}", result.Succeeded);

            if (result.Succeeded == false)
            {
                logger.LogCritical("Login failed.");
                TempData[ErrorMessage] = LogginError;
                return this.View(model);
            }
            logger.LogWarning("Login successful.");
            return this.Redirect(model.ReturnUrl ?? "/Home/Index");
        }
        [HttpPost]
        public async Task<IActionResult> Logout(string? returnUrl = null)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return this.Redirect("/Home/Index");
            // return RedirectToAction("Index", "Home");
        }
        //[HttpGet]
        //public async Task<IActionResult> ProfileSettings()
        //{
        //    var user = await userManager.GetUserAsync(User);

        //    var model = new UserProfileViewModel
        //    {
        //        Email = user.Email,
        //        PhoneNumber = user.PhoneNumber,
        //        FirstName = user.FirstName,
        //        LastName = user.LastName
        //    };

        //    return View(model);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> ProfileSettings(UserProfileViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //        return View(model);

        //    var user = await userManager.GetUserAsync(User);
        //    if (user == null)
        //        return NotFound();

        //    user.FirstName = model.FirstName;
        //    user.LastName = model.LastName;
        //    user.PhoneNumber = model.PhoneNumber;

        //    var result = await userManager.UpdateAsync(user);
        //    if (!result.Succeeded)
        //    {
        //        foreach (var error in result.Errors)
        //            ModelState.AddModelError(string.Empty, error.Description);
        //        return View(model);
        //    }

        //    TempData[SuccessMessage] = "Профилът е обновен успешно!";
        //    return RedirectToAction(nameof(ProfileSettings));
        //}

        [HttpGet]
        public async Task<IActionResult> ProfileSettings(string tab = "Profile")
        {
            switch (tab)
            {
                case "Email":
                    ViewBag.SelectedTab = "Email";
                    return View(new ChangeEmailViewModel());
                    //return PartialView("_EditEmail", new ChangeEmailViewModel());
                case "Password":
                    ViewBag.SelectedTab = "Password";
                    return View(new ChangePasswordViewModel());
                    //return PartialView("_EditPassword", new ChangePasswordViewModel());
                case "TwoFactor":
                    //return PartialView("_TwoFactor");
                case "PersonalData":
                    //return PartialView("_PersonalData");
                case "Profile":
                default:
                    ViewBag.SelectedTab = "Profile";
                    var user = await userManager.GetUserAsync(User);
                    var model = new UserProfileViewModel
                    {
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        FirstName = user.FirstName,
                        LastName = user.LastName
                    };
                    return View(model);
            }
            //ViewBag.SelectedTab = tab;

            //if (tab == "Profile")
            //{
            //    var user = await userManager.GetUserAsync(User);
            //    var model = new UserProfileViewModel
            //    {
            //        Email = user.Email,
            //        PhoneNumber = user.PhoneNumber,
            //        FirstName = user.FirstName,
            //        LastName = user.LastName
            //    };

            //    return View("Manage", model); // manage is the main view, not partial
            //}

            //return View("Manage"); // only tab content is loaded via partial
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(UserProfileViewModel model)
        {
            ViewBag.SelectedTab = "Profile";

            if (!ModelState.IsValid)
                return View("Manage", model);

            var user = await userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PhoneNumber = model.PhoneNumber;

            var result = await userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
                return View("Manage", model);
            }

            TempData["SuccessMessage"] = "Profile updated successfully.";
            return RedirectToAction(nameof(ProfileSettings), new { tab = "Profile" });
        }


        [HttpGet]
        public IActionResult ChangeEmail() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeEmail(ChangeEmailViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await userManager.GetUserAsync(User);
            var passwordValid = await userManager.CheckPasswordAsync(user, model.Password);

            if (!passwordValid)
            {
                ModelState.AddModelError(string.Empty, "Invalid password.");
                return View(model);
            }

            var token = await userManager.GenerateChangeEmailTokenAsync(user, model.NewEmail);
            var result = await userManager.ChangeEmailAsync(user, model.NewEmail, token);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
                return View(model);
            }

            TempData["SuccessMessage"] = "Email updated successfully.";
            return RedirectToAction(nameof(ProfileSettings));
        }

        [HttpGet]
        public IActionResult ChangePassword() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await userManager.GetUserAsync(User);
            var result = await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
                return View(model);
            }

            await signInManager.RefreshSignInAsync(user); // keep user signed in
            TempData["SuccessMessage"] = "Password changed successfully.";
            return RedirectToAction(nameof(ProfileSettings));
        }

        [HttpGet]
        public async Task<IActionResult> LoadTabContent(string tab)
        {
            switch (tab)
            {
                case "Email":
                    return PartialView("_EditEmail", new ChangeEmailViewModel());
                case "Password":
                    return PartialView("_EditPassword", new ChangePasswordViewModel());
                case "TwoFactor":
                    return PartialView("_TwoFactor");
                case "PersonalData":
                    return PartialView("_PersonalData");
                case "Profile":
                default:
                    var user = await userManager.GetUserAsync(User);
                    var model = new UserProfileViewModel
                    {
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        FirstName = user.FirstName,
                        LastName = user.LastName
                    };
                    return PartialView("_Profile", model);
            }
        }

        // Примерни действия, ако искаш отделни заявки за всяка секция:
        //public IActionResult EditEmail() => View();
        //public IActionResult EditPassword() => View();
        public IActionResult TwoFactor() => View();
        public IActionResult PersonalData() => View();
    }
}

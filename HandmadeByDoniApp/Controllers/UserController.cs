
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
//using HandmadeByDoniApp.Web.Resources;
using HandmadeByDoniApp.Services.Data.Interfaces;
using Microsoft.Extensions.Localization;
using Resources.Resources;


namespace HandmadeByDoniApp.Web.Controllers
{


    public class UserController : BaseController<UserController>
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMemoryCache memoryCache;
        private readonly ILogger<UserController> logger;
        private IEmailService emailService;
        private IStringLocalizer<App> L;

        public UserController(SignInManager<ApplicationUser> signInManager,
                              UserManager<ApplicationUser> userManager,
                              IMemoryCache memoryCache,
                              ILogger<UserController> logger,
                              IEmailService emailService,
                               IStringLocalizer<App> l)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;

            this.memoryCache = memoryCache;
            this.logger = logger;
            this.emailService = emailService;
            this.L = l;
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
                this.TempData[ErrorMessage] = L["FillAllFields"].Value;
                return this.View(model);
            }

            ApplicationUser user = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,                
               
            };

            IdentityResult result =
                await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded == false)
            {
                user = await userManager.FindByEmailAsync(model.Email);
                if (user == null || user.EmailConfirmed)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return this.View(model);
                }        
            }

            string token = await userManager.GenerateEmailConfirmationTokenAsync(user);

            string body = emailService.GetConfirmEmail(token, user);
            bool res = await emailService.SendEmailAsync(model.Email, L["ConfirmEmail"].Value, body);

            if (res)
            {
                this.TempData[SuccessMessage] = L["EmailSentSuccess"].Value;               
            }
            else
            {
                this.TempData[ErrorMessage] = L["ЕmailSendError"].Value;
            }

            // await signInManager.SignInAsync(user, false);

            return this.RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string? email = null, string? token=null)
        {
            if(string.IsNullOrEmpty(email)|| string.IsNullOrEmpty(token))
            {
                this.TempData[ErrorMessage] = L["UnexpectedError"].Value;
                return this.RedirectToAction("Index", "Home");
            }
        
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                TempData[ErrorMessage] = L["UserNotFound"].Value;
                return RedirectToAction("Index", "Home");
            }
            var result = await userManager.ConfirmEmailAsync(user, token);

            if(!result.Succeeded)
            {
                this.TempData[ErrorMessage] = L["UnexpectedError"].Value;
                return this.RedirectToAction("Index", "Home");
            }

            var model = new ConfirmEmailViewModel
            {
                Email = email,
                Token = string.Empty
            };

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string? returnUrl = null)
        {
            //logger.LogWarning("Login page accessed.");
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            LoginFormModel model = new LoginFormModel()
            {
                ReturnUrl = returnUrl
            };
            //logger.LogWarning("Login page model created.");
            return this.View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginFormModel model)
        {
            //logger.LogWarning("Login post action accessed.");
            if (ModelState.IsValid == false)
            {
                //logger.LogCritical("Model state is not valid.");
                TempData[ErrorMessage] = L["FillAllFields"].Value;
                return this.View(model);
            }
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user != null && user.EmailConfirmed == false)
            {
                TempData[ErrorMessage] = "ConfirmEmailPrompt";
                return this.Redirect(model.ReturnUrl ?? "/Home/Index");
            }

            //logger.LogWarning("Model state is valid.");
            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            //logger.LogWarning("Password sign in result: {result}", result.Succeeded);

            if (result.Succeeded == false)
            {
                //logger.LogCritical("Login failed.");
                TempData[ErrorMessage] = L["LogginError"].Value;
                return this.View(model);
            }
            //logger.LogWarning("Login successful.");
            return this.Redirect(model.ReturnUrl ?? "/Home/Index");
        }
       
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            try
            {
                //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            }

            catch (Exception ex)
            {
                logger.LogWarning($"Logout Error: {ex.Message}");
            }
            // return this.Redirect("/Home/Index");
            return RedirectToAction("Index", "Home");
        }
  
        [HttpGet]
        public async Task<IActionResult> ProfileSettings(string tab = "Profile")
        {
            ViewBag.SelectedTab = tab;
            switch (tab)
            {
                case "Email":
                    return View(new ChangeEmailViewModel());
                case "Password":
                    return View(new ChangePasswordViewModel());
                case "TwoFactor":
                    //return PartialView("_TwoFactor");
                case "Profile":
                default:

                    if (tab == "TwoFactor")
                    {
                        this.TempData[ErrorMessage] = "В процес на разработка";
                    }

                    ViewBag.SelectedTab = "Profile";
                    var user = await userManager.GetUserAsync(User);
                    var model = new UserProfileViewModel
                    {
                        Email = user.Email,
                        //PhoneNumber = user.PhoneNumber,
                        FirstName = user.FirstName,
                        LastName = user.LastName
                    };
                    return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(UserProfileViewModel model)
        {
            try
            {
                ViewBag.SelectedTab = "Profile";

                if (!ModelState.IsValid)
                {
                    this.TempData[ErrorMessage] = L["FillAllFields"].Value;
                    return RedirectToAction(nameof(ProfileSettings), new { tab = "Profile" });
                }

                var user = await userManager.GetUserAsync(User);
                if (user == null) return NotFound();

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                //user.PhoneNumber = model.PhoneNumber;

                var result = await userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);

                    this.TempData[ErrorMessage] = L["UnexpectedError"].Value;
                    return RedirectToAction(nameof(ProfileSettings), new { tab = "Profile" });
                }

                TempData[SuccessMessage] = L["UpdatedSuccessfully"].Value;
                return RedirectToAction(nameof(ProfileSettings), new { tab = "Profile" });
            }
            catch (Exception ex)
            {
                logger.LogWarning($"UpdateProfile Error: {ex.Message}");
                this.TempData[ErrorMessage] = L["UnexpectedError"].Value;
                return RedirectToAction(nameof(ProfileSettings), new { tab = "Profile" });
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeEmail(ChangeEmailViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    this.TempData[ErrorMessage] = L["FillAllFields"].Value;
                    return RedirectToAction(nameof(ProfileSettings), new { tab = "Email" });
                }

                var user = await userManager.GetUserAsync(User);
                var passwordValid = await userManager.CheckPasswordAsync(user, model.Password);

                if (!passwordValid)
                {
                    this.TempData[ErrorMessage] = L["InvalidPassword"].Value;
                    return RedirectToAction(nameof(ProfileSettings), new { tab = "Email" });
                }

                var token = await userManager.GenerateChangeEmailTokenAsync(user, model.NewEmail);
                var result = await userManager.ChangeEmailAsync(user, model.NewEmail, token);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError("", error.Description);

                    this.TempData[ErrorMessage] = L["UnexpectedError"].Value;
                    return RedirectToAction(nameof(ProfileSettings), new { tab = "Email" });
                }

                TempData[SuccessMessage] = L["UpdatedSuccessfully"].Value;
                return RedirectToAction(nameof(ProfileSettings));
            }
            catch (Exception ex)
            {
                logger.LogWarning($"ChangeEmail Error: {ex.Message}");
                this.TempData[ErrorMessage] = L["UnexpectedError"].Value;
                return RedirectToAction(nameof(ProfileSettings), new { tab = "Email" });
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    this.TempData[ErrorMessage] = L["FillAllFields"].Value;
                    return RedirectToAction(nameof(ProfileSettings), new { tab = "Password" });
                }

                var user = await userManager.GetUserAsync(User);
                var result = await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError("", error.Description);

                    this.TempData[ErrorMessage] = L["UnexpectedError"].Value;
                    return RedirectToAction(nameof(ProfileSettings), new { tab = "Password" });

                }

                await signInManager.RefreshSignInAsync(user); // keep user signed in
                TempData[SuccessMessage] = L["UpdatedSuccessfully"].Value;
                return RedirectToAction(nameof(ProfileSettings));
            }
            catch (Exception ex)
            {
                logger.LogWarning($"ChangePassword Error: {ex.Message}");
                this.TempData[ErrorMessage] = L["UnexpectedError"].Value;
                return RedirectToAction(nameof(ProfileSettings), new { tab = "Password" });
            }
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TwoFactor()
        {         
            return RedirectToAction(nameof(ProfileSettings));
        }
    }
}

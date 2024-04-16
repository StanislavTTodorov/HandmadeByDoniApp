using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.ViewModels.User;
using Microsoft.AspNetCore.Mvc;
using static HandmadeByDoniApp.Common.NotificationMessagesConstants;
using static HandmadeByDoniApp.Common.GeneralMessages;

namespace HandmadeByDoniApp.Web.Areas.Admin.Controllers
{
    public class UserController : BaseAdminController
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<IActionResult> All()
        {
            try
            {
                IEnumerable<UserViewModel> userViewModels = await this.userService.AllUsersAsync();
                return View(userViewModels);
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = UnexpectedError;
                return this.RedirectToAction("Index", "Home", new { area = "" });

            }

        }
    }
}

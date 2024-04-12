using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

namespace HandmadeByDoniApp.Web.Areas.Admin.Controllers
{
    public class UserController : BaseAdminController
    {
        private readonly IUserService userService;
        public UserController(IUserService  userService)
        {
            this.userService = userService;
        }

        public async Task<IActionResult> All()
        {
            IEnumerable<UserViewModel> userViewModels = await this.userService.AllUsersAsync();
            return View(userViewModels);
        }
    }
}

using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.Infrastructure.Extensions;
using HandmadeByDoniApp.Web.ViewModels.Order;
using Microsoft.AspNetCore.Mvc;
using static HandmadeByDoniApp.Common.GeneralApplicationConstants;
using static HandmadeByDoniApp.Common.NotificationMessagesConstants;

namespace HandmadeByDoniApp.Web.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService orderService;
        private readonly IUserService userService;
        public OrderController(IOrderService orderService,
                               IUserService userService)
        {
            this.orderService = orderService;
            this.userService = userService;

        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            try
            {
                string userId = User.GetId();
                MineProductViewModel productViewModel = await this.orderService.AllMineProductsAsync(userId);
                return this.View(productViewModel);
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = "Unexpected error occurred while trying to open Cart itams ! Please try agenin later.";
               return this.RedirectToAction("Index", "Home" ,new { area = "" });
            }           
        }

        [HttpGet]
        public async  Task<IActionResult> Add(string id)
        {
            string userId = User.GetId();
            await this.userService.AddProductByUserIdAsync(userId, id);

            return this.RedirectToAction("Mine", "Order", new { area = "" });
        }
    }
}

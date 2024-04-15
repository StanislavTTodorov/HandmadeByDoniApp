using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.ViewModels.Order;
using Microsoft.AspNetCore.Mvc;
using static HandmadeByDoniApp.Common.NotificationMessagesConstants;

namespace HandmadeByDoniApp.Web.Areas.Admin.Controllers
{
    public class OrderController : BaseAdminController
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }
        [HttpGet]
        public async Task<IActionResult> UsersOrders()
        {
            try
            {
                ICollection<AdminOrdersViewModel> viewModel = await this.orderService.GetUserOrdersAsync();

                return this.View(viewModel);
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = "Unexpected error occurred while trying to open Details Order! Please try again later.";
                return this.RedirectToAction("Index", "Home", new { area = "" });
            }
        }
        [HttpGet]
        public async Task<IActionResult> Sent(string orderId)
        {
            bool isExists = await this.orderService.UserOrderExistsByOrderIdAsync(orderId);
            if (isExists == false)
            {
                this.TempData[ErrorMessage] = "Order with the provided id does not exist!";
                return this.RedirectToAction("UsersOrders", "Order");
            }
            try
            {
                await this.orderService.EditSentToTrueAsync(orderId);
                this.TempData[ErrorMessage] = "Sent successfully!";
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = "Unexpected error occurred while trying to open Details Order! Please try again later.";
            }
            return this.RedirectToAction("UsersOrders", "Order", new { area = "Admin" });
        }
    }
}

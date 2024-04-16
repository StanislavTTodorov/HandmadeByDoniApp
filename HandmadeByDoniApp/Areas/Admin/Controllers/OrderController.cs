using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.ViewModels.Order;
using Microsoft.AspNetCore.Mvc;
using static HandmadeByDoniApp.Common.NotificationMessagesConstants;
using static HandmadeByDoniApp.Common.GeneralMessages;
using HandmadeByDoniApp.Data.Migrations;


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
                this.TempData[ErrorMessage] = string.Format(UnexpectedErrorTryingTo, "open Details Order");
                return this.RedirectToAction("Index", "Home", new { area = "" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Sent(string orderId)
        {
            bool isExists = await this.orderService.UserOrderExistsByOrderIdAsync(orderId);
            if (isExists == false)
            {
                this.TempData[ErrorMessage] = string.Format(ProductNotExist, nameof(Order));
                return this.RedirectToAction("UsersOrders", "Order");
            }

            try
            {
                await this.orderService.EditSentToTrueAsync(orderId);
                this.TempData[SuccessMessage] = "Sent successfully!";
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = string.Format(UnexpectedErrorTryingTo, "open Details Order");
            }
            return this.RedirectToAction("UsersOrders", "Order", new { area = "Admin" });
        }
        [HttpGet]
        public async Task<IActionResult> CancelOrder(string id)
        {
            bool isExists = await this.orderService.UserOrderExistsByOrderIdAsync(id);
            if (isExists == false)
            {
                this.TempData[ErrorMessage] = string.Format(ProductNotExist, nameof(Order));
                return this.RedirectToAction("UsersOrders", "Order", new { area = "Admin" });
            }           

            try
            {
                await this.orderService.DeleteUserOrderByOrderIdAsync(id);
                this.TempData[SuccessMessage] = string.Format(CancelSuccessfully, nameof(Order));
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = string.Format(UnexpectedErrorTryingTo, "Cancel Order");
            }
            return this.RedirectToAction("UsersOrders", "Order", new { area = "Admin" });
        }
    }
}

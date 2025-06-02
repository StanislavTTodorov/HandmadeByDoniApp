using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.ViewModels.Order;
using Microsoft.AspNetCore.Mvc;
using static HandmadeByDoniApp.Common.NotificationMessagesConstants;
using static HandmadeByDoniApp.Common.GeneralMessages;
using HandmadeByDoniApp.Data.Models;
using Microsoft.Extensions.Localization;


namespace HandmadeByDoniApp.Web.Areas.Admin.Controllers
{
    public class OrderController : BaseAdminController<OrderController>
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
                this.TempData[ErrorMessage] = string.Format(ProductNotExist, nameof(Data.Migrations.Order));
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
                this.TempData[ErrorMessage] = string.Format(ProductNotExist, nameof(Data.Migrations.Order));
                return this.RedirectToAction("UsersOrders", "Order", new { area = "Admin" });
            }           

            try
            {
                await this.orderService.DeleteUserOrderByOrderIdAsync(id);
                this.TempData[SuccessMessage] = string.Format(CancelSuccessfully, nameof(Data.Migrations.Order));
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = string.Format(UnexpectedErrorTryingTo, "Cancel Order");
            }
            return this.RedirectToAction("UsersOrders", "Order", new { area = "Admin" });
        }

        [HttpGet]
        public async Task<IActionResult> AddShipmentNoteNumber(string id)
        {
            bool isExists = await this.orderService.UserOrderExistsByOrderIdAsync(id);
            if (isExists == false)
            {
                this.TempData[ErrorMessage] = L["NotHaveOrdars"];//NotHaveOrdars;
                return this.RedirectToAction("UsersOrders", "Order", new { area = "Admin" });
            }

            try
            {
                EditOrderViewModel formModel = await this.orderService.GetUserOrderByOrdeIdAsync(id);
                //return this.View("~/Admin/Views/Order/AddShipmentNoteNumber.cshtml", formModel);
                return View(formModel);
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = L["UnexpectedError"];//UnexpectedError
                return this.RedirectToAction("UsersOrders", "Order", new { area = "Admin" });
            }         
        }

        [HttpPost]
        public async Task<IActionResult> AddShipmentNoteNumber(EditOrderViewModel formModel  /*string id,string shipmentNoteNumber*//*EditOrderViewModel formModel*/)
        {

            bool isExists = await this.orderService.UserOrderExistsByOrderIdAsync(/*id*/formModel.Id);
            if (isExists == false)
            {
                this.TempData[ErrorMessage] = L["NotHaveOrdars"];//NotHaveOrdars;
                return this.RedirectToAction("UsersOrders", "Order", new { area = "Admin" });
            }

            try
            {
                await this.orderService.EditSentToTrueAsync(/*id, shipmentNoteNumber */formModel.Id, formModel.ShipmentNoteNumber);
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, string.Format(UnexpectedErrorTryingTo, $"add The {nameof(Product)}"));
                this.TempData[ErrorMessage] = $"{L["UnexpectedErrorTryingTo"]} {L["addThe"]} {L["ShipmentNoteNumber"]}";

                return this.RedirectToAction("UsersOrders", "Order", new { area = "Admin" });
                // return this.View(formModel);
            }

            this.TempData[SuccessMessage] = string.Format(EditSuccessfully, $"{L["ShipmentNoteNumber"]}");
            return this.RedirectToAction("UsersOrders", "Order", new { area = "Admin" });
        }

    }
}

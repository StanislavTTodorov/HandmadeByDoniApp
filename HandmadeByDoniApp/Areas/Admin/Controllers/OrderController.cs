using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.ViewModels.Order;
using Microsoft.AspNetCore.Mvc;
using static HandmadeByDoniApp.Common.NotificationMessagesConstants;
using static HandmadeByDoniApp.Common.GeneralMessages;
using HandmadeByDoniApp.Data.Models;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using AngleSharp.Dom;
using Resources.Resources;


namespace HandmadeByDoniApp.Web.Areas.Admin.Controllers
{
    public class OrderController : BaseAdminController<OrderController>
    {
        private readonly IOrderService orderService;
        private readonly IStringLocalizer<App> L;

        public OrderController(IOrderService orderService, IStringLocalizer<App> l)
        {
            this.orderService = orderService;
            this.L = l;
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
                this.TempData[ErrorMessage] = L["NotHaveOrdars"].Value;//NotHaveOrdars;
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
                this.TempData[ErrorMessage] = L["UnexpectedError"].Value;//UnexpectedError
                return this.RedirectToAction("UsersOrders", "Order", new { area = "Admin" });
            }         
        }

        [HttpPost]
        public async Task<IActionResult> AddShipmentNoteNumber(EditOrderViewModel formModel)
        {

            bool isExists = await this.orderService.UserOrderExistsByOrderIdAsync(/*id*/formModel.Id);
            if (isExists == false)
            {
                this.TempData[ErrorMessage] = L["NotHaveOrdars"].Value;//NotHaveOrdars;
                return this.RedirectToAction("UsersOrders", "Order", new { area = "Admin" });
            }

            try
            {
                await this.orderService.EditSentToTrueAsync(/*id, shipmentNoteNumber */formModel.Id, formModel.ShipmentNoteNumber);
            }
            catch (Exception)
            {               
                this.TempData[ErrorMessage] = $"{L["UnexpectedErrorTryingTo"].Value} {L["addThe"].Value} {L["ShipmentNoteNumber"].Value}";
                this.ModelState.AddModelError(string.Empty, $"{L["UnexpectedErrorTryingTo"].Value} {L["addThe"].Value} {L["ShipmentNoteNumber"].Value}");

                return this.RedirectToAction("UsersOrders", "Order", new { area = "Admin" });
                // return this.View(formModel);
            }

            this.TempData[SuccessMessage] = $"{L["EditSuccessfully"].Value} {L["ShipmentNoteNumber"].Value}"  ;//  string.Format(EditSuccessfully, $"{L["ShipmentNoteNumber"].Value}");
            return this.RedirectToAction("UsersOrders", "Order", new { area = "Admin" });
        }

    }
}

﻿using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.ViewModels.Order;
using Microsoft.AspNetCore.Mvc;
using static HandmadeByDoniApp.Common.NotificationMessagesConstants;
using static HandmadeByDoniApp.Common.GeneralMessages;
using HandmadeByDoniApp.Web.Resources;
using HandmadeByDoniApp.Data.Models;


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
        public async Task<IActionResult> EditOrderIdAsync(string orderId)
        {
            bool isExists = await this.orderService.UserOrderExistsByOrderIdAsync(orderId);
            if (isExists == false)
            {
                this.TempData[ErrorMessage] = App.L("NotHaveOrdars");//NotHaveOrdars;
                return this.RedirectToAction("UsersOrders", "Order", new { area = "Admin" });
            }

            try
            {
                EditOrderViewModel formModel = await this.orderService.GetUserOrderByOrdeIdAsync(orderId);
                return View(formModel);
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = App.L("UnexpectedError");//UnexpectedError
                return this.RedirectToAction("UsersOrders", "Order", new { area = "Admin" });
            }         
        }

        [HttpPost]
        public async Task<IActionResult> EditOrderIdAsync(AdminOrdersViewModel formModel  /*string orderId,string shipmentNoteNumber*//*EditOrderViewModel formModel*/)
        {

            bool isExists = await this.orderService.UserOrderExistsByOrderIdAsync(/*orderId*/formModel.OrderId);
            if (isExists == false)
            {
                this.TempData[ErrorMessage] = App.L("NotHaveOrdars");//NotHaveOrdars;
                return this.RedirectToAction("UsersOrders", "Order", new { area = "Admin" });
            }

            try
            {
                await this.orderService.EditSentToTrueAsync(/*orderId, shipmentNoteNumber */formModel.OrderId, formModel.ShipmentNoteNumber);
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, string.Format(UnexpectedErrorTryingTo, $"add The {nameof(Product)}"));
                this.TempData[ErrorMessage] = $"{App.L("UnexpectedErrorTryingTo")} {App.L("addThe")} {App.L("ShipmentNoteNumber")}";

                return this.RedirectToAction("UsersOrders", "Order", new { area = "Admin" });
                // return this.View(formModel);
            }

            this.TempData[SuccessMessage] = string.Format(EditSuccessfully, $"{App.L("ShipmentNoteNumber")}");
            return this.RedirectToAction("UsersOrders", "Order", new { area = "Admin" });
        }

    }
}

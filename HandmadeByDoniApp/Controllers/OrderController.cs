using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.Infrastructure.Extensions;
using HandmadeByDoniApp.Web.ViewModels.Order;
using Microsoft.AspNetCore.Mvc;
using static HandmadeByDoniApp.Common.NotificationMessagesConstants;

namespace HandmadeByDoniApp.Web.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;


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
                this.TempData[ErrorMessage] = "Unexpected error occurred while trying to open Shop List! Please try agenin later.";
                return this.RedirectToAction("Index", "Home", new { area = "" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Add(string id)
        {
            try
            {
                string userId = User.GetId();
                await this.orderService.AddProductByUserIdAsync(userId, id);
                TempData[SuccessMessage] = "Product was added to Shop List successfully!";
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = "Unexpected error occurred while trying to Add in Shop List! Please try agenin later.";
                return this.RedirectToAction("Index", "Home", new { area = "" });
            }
           
            return this.RedirectToAction("Mine", "Order", new { id });
        }
        [HttpGet]
        public async Task<IActionResult> Remove(string id)
        {
            try
            {
                string userId = User.GetId();
                await this.orderService.RemoveProductByUserIdAsync(userId, id);
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = "Unexpected error occurred while trying to remove in Shop List ! Please try agenin later.";
                return this.RedirectToAction("Mine", "Order", new { area = "" });
            }

            return this.RedirectToAction("Mine", "Order", new { area = "" });
        }
    }
}

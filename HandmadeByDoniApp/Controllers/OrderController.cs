using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.Infrastructure.Extensions;
using HandmadeByDoniApp.Web.ViewModels.Order;
using HandmadeByDoniApp.Web.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Caching.Memory;
using static HandmadeByDoniApp.Common.NotificationMessagesConstants;
using static HandmadeByDoniApp.Common.GeneralApplicationConstants;


namespace HandmadeByDoniApp.Web.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService orderService;
        private readonly IMemoryCache memoryCache;

        public OrderController(IOrderService orderService,
            IMemoryCache memoryCache)
        {
            this.orderService = orderService;
            this.memoryCache = memoryCache;


        }
        public List<ProductsAllViewModel> cartItems = null!;


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
            bool isInSet = await this.orderService.ExistsInSetByIdAsync(id);
            if (isInSet)
            {
                this.TempData[ErrorMessage] = "The product is in Set, you cannot buy it separately. You can see Set from \"See Set\"";
                return this.RedirectToAction("Details", "Product", new { area = "", id });
            }
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

        [HttpGet]
        public async Task<IActionResult> AddOrder()
        {
            string userId = User.GetId();

			return this.RedirectToAction("Mine", "Order", new { area = "" });
        }
    }
}

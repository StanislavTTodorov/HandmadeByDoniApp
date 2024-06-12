using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.Infrastructure.Extensions;
using HandmadeByDoniApp.Web.ViewModels.Order;
using Microsoft.AspNetCore.Mvc;
using static HandmadeByDoniApp.Common.NotificationMessagesConstants;
using static HandmadeByDoniApp.Common.GeneralMessages;
using HandmadeByDoniApp.Data.Models;

namespace HandmadeByDoniApp.Web.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService orderService;
        private readonly IAddressService addressService;

        public OrderController(
            IOrderService orderService,
            IAddressService addressService)
        {
            this.orderService = orderService;
            this.addressService = addressService;
        }      

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            try
            {
                string userId = User.GetId();
                MineProductViewModel productViewModel = await this.orderService.AllMineProductsByUserIdAsync(userId);
                return this.View(productViewModel);
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = string.Format(UnexpectedErrorTryingTo, "open Cart");

                return this.RedirectToAction("Index", "Home", new { area = "" });
            }

        }

        [HttpGet]
        public async Task<IActionResult> Add(string id, string returnUrl)
        {           
            bool isActive = await this.orderService.IsActiveByIdAsync(id);
            if (isActive==false)
            {
                this.TempData[InformationMessage] = ProductNotAvailable;
                return this.Redirect(returnUrl);
			}

            try
			{
                string userId = User.GetId();
                await this.orderService.AddProductByUserIdAsync(userId, id);
                this.TempData[SuccessMessage] = AddProductSuccessfully;
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = string.Format(UnexpectedErrorTryingTo, $"add new {nameof(Order)}");
                return this.Redirect(returnUrl);
            }

            return this.Redirect(returnUrl);          
        }

        [HttpGet]
        public async Task<IActionResult> Remove(string id)
        {
            try
            {
                string userId = User.GetId();
                await this.orderService.RemoveProductByUserIdAsync(userId, id);
                this.TempData[SuccessMessage] = string.Format(RemoveSuccessfully, $"{nameof(Product)}");
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = string.Format(UnexpectedErrorTryingTo, "remove from Cart");
                return this.RedirectToAction("Mine", "Order", new { area = "" });
            }

            return this.RedirectToAction("Mine", "Order", new { area = "" });
        }

        [HttpGet]
        public async Task<IActionResult> AddOrder()
        {
            string userId = User.GetId();
            bool isExists = await this.addressService.ExistsByUserIdAsync(userId);
            if (isExists == false)
            {
                this.TempData[ErrorMessage] = NotHaveAddress;
                return this.RedirectToAction("Add", "Address", new { area = "" });
            }

            try
            {
                bool isAllActiv = await this.orderService.CreateRegisterUserOrderByUserIdAsync(userId);
                
                if (isAllActiv)
                {
                    TempData[SuccessMessage] = string.Format(AddSuccessfully, nameof(Order));                
                }
                else
                {
                    this.TempData[ErrorMessage] = ProductNotAvailable;
                }
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = string.Format(UnexpectedErrorTryingTo, "Register Order");
            }
            
			return this.RedirectToAction("Mine", "Order", new { area = "" });
        }

        [HttpGet]
        public async Task<IActionResult> DetailsOrder()
        {         
            try
            {
                string userId = User.GetId();
                DetailsOrdarViewModel viewModel = new DetailsOrdarViewModel()
                {
                    MineProduct = await this.orderService.AllMineProductsByUserIdAsync(userId),
                    Address = await this.addressService.GetAddressByUserIdAsync(userId)
                };
                         
                return this.View(viewModel);
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = string.Format(UnexpectedErrorTryingTo, "open Details Order");
                return this.RedirectToAction("Mine", "Order", new { area = "" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> OrderStatus()
        {
            string userId = User.GetId();
            bool isExists = await this.orderService.UserOrderExistsByUserIdAsync(userId);
            if (isExists == false)
            {
                this.TempData[InformationMessage] = NotHaveOrdars;
                return this.RedirectToAction("All", "Product", new { area = "" });
            }

            try
            {
                ICollection<OrderStatusViewModel> viewModel = await this.orderService.GetUserOrdersByUserIdAsync(userId);
            
                return this.View(viewModel);
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = string.Format(UnexpectedErrorTryingTo, "open Order Status");
                return this.RedirectToAction("Mine", "Order", new { area = "" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ProductOrder(string id)
        {
            try
            {
                MineProductViewModel productViewModel = await this.orderService.AllOrderProductsByOrderIdAsync(id);
                return this.View(productViewModel);
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = string.Format(UnexpectedErrorTryingTo, "open Order Product");
                return this.RedirectToAction("Index", "Home", new { area = "" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> CancelOrder(string id)
        {
            bool isExists = await this.orderService.UserOrderExistsByOrderIdAsync(id);
            if (isExists == false)
            {
                this.TempData[ErrorMessage] = string.Format(ProductNotExist,nameof(Order));
                return this.RedirectToAction("OrderStatus", "Order" ,new { areas=""});
            }

            bool isSent= await this.orderService.UserOrderIsSentByOrderIdAsync(id);
            if (isSent == true)
            {
                this.TempData[ErrorMessage] = OrdarIsSent;
                return this.RedirectToAction("OrderStatus", "Order", new { areas = "" });
            }

            try 
            {
                await this.orderService.DeleteUserOrderByOrderIdAsync(id);
                this.TempData[SuccessMessage] = string.Format(CancelSuccessfully,nameof(Order));
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = string.Format(UnexpectedErrorTryingTo, "Cancel Order");
            }
            return this.RedirectToAction("OrderStatus", "Order", new { area = "" });
        }
    }
}

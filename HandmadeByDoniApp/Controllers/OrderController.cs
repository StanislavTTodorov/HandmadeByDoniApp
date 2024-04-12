﻿using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.Infrastructure.Extensions;
using HandmadeByDoniApp.Web.ViewModels.Order;
using HandmadeByDoniApp.Web.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Caching.Memory;
using static HandmadeByDoniApp.Common.NotificationMessagesConstants;
using static HandmadeByDoniApp.Common.GeneralApplicationConstants;
using HandmadeByDoniApp.Web.ViewModels.Address;


namespace HandmadeByDoniApp.Web.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService orderService;
        private readonly IMemoryCache memoryCache;
        private readonly IAddressService addressService;

        public OrderController(
            IOrderService orderService,
            IMemoryCache memoryCache,
            IAddressService addressService)
        {
            this.orderService = orderService;
            this.memoryCache = memoryCache;
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
                this.TempData[ErrorMessage] = "Unexpected error occurred while trying to open Shop List! Please try agenin later.";
                return this.RedirectToAction("Index", "Home", new { area = "" });
            }

        }

        [HttpGet]
        public async Task<IActionResult> Add(string id, string returnUrl)
        {
            bool isInSet = await this.orderService.ExistsInSetByIdAsync(id);
            if (isInSet)
            {
                this.TempData[InformationMessage] = "The product is in Set, you cannot buy it separately. You can see Set from \"See Set\"";
                return this.RedirectToAction("Details", "Product", new { area = "", id });
            }

            bool isActive = await this.orderService.IsActiveByIdAsync(id);
            if (isActive==false)
            {
                this.TempData[InformationMessage] = "Product is not Availability";
                return this.Redirect(returnUrl);
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
                return this.Redirect(returnUrl);
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
            bool isExists = await this.addressService.ExistsByUserIdAsync(userId);
            if (isExists == false)
            {
                TempData[ErrorMessage] = "You not have Address! You can add your address here";
                return RedirectToAction("Add", "Address", new { area = "" });
            }
            try
            {
                bool isAllActiv = await this.orderService.CreateRegisterUserOrderByUserIdAsync(userId);
                
                if (isAllActiv)
                {
                    TempData[SuccessMessage] = "Order was added successfully!";                   
                }
                else
                {
                    this.TempData[ErrorMessage] = "Some of the products are not for sale";
                }
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = "Unexpected error occurred while trying to remove in Shop List ! Please try agenin later.";
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
                this.TempData[ErrorMessage] = "Unexpected error occurred while trying to open Details Order! Please try agenin later.";
                return this.RedirectToAction("Mine", "Order", new { area = "" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> OrderStatus()
        {
            string userId = User.GetId();
            bool isExists = await this.orderService.ExistsByUserIdAsync(userId);
            if (isExists == false)
            {
                TempData[ErrorMessage] = "You not have Orders! You can product from here";
                return RedirectToAction("All", "Product", new { area = "" });
            }
            try
            {
                ICollection<OrderStatusViewModel> viewModel = await this.orderService.GetUserOrdersByUserIdAsync(userId);
            
                return this.View(viewModel);
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = "Unexpected error occurred while trying to open Details Order! Please try agenin later.";
                return this.RedirectToAction("Mine", "Order", new { area = "" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ProductOrder(string id)
        {
            try
            {
                string userId = User.GetId();
                MineProductViewModel productViewModel = await this.orderService.AllOrderProductsByUserIdAsync(userId,id);
                return this.View(productViewModel);
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = "Unexpected error occurred while trying to open Order Product! Please try agenin later.";
                return this.RedirectToAction("Index", "Home", new { area = "" });
            }

        }
    }
}

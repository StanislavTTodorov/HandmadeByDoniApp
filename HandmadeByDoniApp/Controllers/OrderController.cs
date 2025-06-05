using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.Infrastructure.Extensions;
using HandmadeByDoniApp.Web.ViewModels.Order;
using Microsoft.AspNetCore.Mvc;
using static HandmadeByDoniApp.Common.NotificationMessagesConstants;
using static HandmadeByDoniApp.Common.GeneralMessages;
using HandmadeByDoniApp.Data.Models;
//using HandmadeByDoniApp.Web.Resources;
using Microsoft.Extensions.Localization;
using Resources.Resources;

namespace HandmadeByDoniApp.Web.Controllers
{
    public class OrderController : BaseController<OrderController>
    {
        private readonly IOrderService orderService;
        private readonly IAddressService addressService;
        private IStringLocalizer<App> L;

        public OrderController(
            IOrderService orderService,
            IAddressService addressService, 
            IStringLocalizer<App> l)
        {
            this.orderService = orderService;     
            this.addressService = addressService;
            this.L = l;
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
                this.TempData[ErrorMessage] = $"{L["UnexpectedErrorTryingTo"].Value} {L["openCart"].Value}"; //string.Format(UnexpectedErrorTryingTo, "open Cart");

                return this.RedirectToAction("Index", "Home", new { area = "" });
            }

        }

        [HttpGet]
        public async Task<IActionResult> Add(string id, string returnUrl)
        {           
            bool isActive = await this.orderService.IsActiveByIdAsync(id);
            if (isActive==false)
            {
                this.TempData[InformationMessage] = L["ProductNotAvailable"].Value;// ProductNotAvailable;
                return this.Redirect(returnUrl);
			}

            try
			{
                string userId = User.GetId();
                await this.orderService.AddProductByUserIdAsync(userId, id);
                this.TempData[SuccessMessage] = L["AddProductSuccessfully"].Value;// AddProductSuccessfully; ///////////////////////////////////////////////////
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = $"{L["UnexpectedErrorTryingTo"].Value} {L["addNew"].Value}  {L[nameof(Order)].Value}"; //string.Format(UnexpectedErrorTryingTo, $"add new {nameof(Order)}");
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
                this.TempData[SuccessMessage] = $"{L[nameof(Product)].Value} {L["RemoveSuccessfully"].Value}";// string.Format(RemoveSuccessfully, $"{nameof(Product)}");
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = $"{L["UnexpectedErrorTryingTo"].Value} {L["removeFromCart"].Value}";  //string.Format(UnexpectedErrorTryingTo, "remove from Cart");
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
                this.TempData[ErrorMessage] = L["NotHaveAddress"].Value; //NotHaveAddress;
                return this.RedirectToAction("Add", "Address", new { area = "" });
            }

            try
            {
                bool isAllActiv = await this.orderService.CreateRegisterUserOrderByUserIdAsync(userId);
                
                if (isAllActiv)
                {
                    TempData[SuccessMessage] = $"{L[nameof(Order)].Value} {L["AddSuccessfully"].Value}";// string.Format(AddSuccessfully, nameof(Order));                
                }
                else
                {
                    this.TempData[ErrorMessage] = L["ProductNotAvailable"].Value;// ProductNotAvailable ;
                }
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = $"{L["UnexpectedErrorTryingTo"].Value} {L["RegisterOrder"].Value}"; //string.Format(UnexpectedErrorTryingTo, "Register Order");
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
                this.TempData[ErrorMessage] = $"{L["UnexpectedErrorTryingTo"].Value} {L["openDetailsOrder"].Value}"; //string.Format(UnexpectedErrorTryingTo, "open Details Order");
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
                this.TempData[InformationMessage] = L["NotHaveOrdars"].Value;//NotHaveOrdars;
                return this.RedirectToAction("All", "Product", new { area = "" });
            }

            try
            {
                ICollection<OrderStatusViewModel> viewModel = await this.orderService.GetUserOrdersByUserIdAsync(userId);
            
                return this.View(viewModel);
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = $"{L["UnexpectedErrorTryingTo"].Value} {L["openOrderStatus"].Value}"; //string.Format(UnexpectedErrorTryingTo, "open Order Status");
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
                this.TempData[ErrorMessage] = $"{L["UnexpectedErrorTryingTo"].Value}   {L["openOrderProduct"].Value}";// string.Format(UnexpectedErrorTryingTo, "open Order Product");
                return this.RedirectToAction("Index", "Home", new { area = "" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> CancelOrder(string id)
        {
            bool isExists = await this.orderService.UserOrderExistsByOrderIdAsync(id);
            if (isExists == false)
            {
                this.TempData[ErrorMessage] = $"{L[nameof(Order)].Value}   {L["ProductNotExist"].Value}";// string.Format(ProductNotExist,nameof(Order));
                return this.RedirectToAction("OrderStatus", "Order" ,new { areas=""});
            }

            bool isSent= await this.orderService.UserOrderIsSentByOrderIdAsync(id);
            if (isSent == true)
            {
                this.TempData[ErrorMessage] = L["OrdarIsSent"].Value; //OrdarIsSent;
                return this.RedirectToAction("OrderStatus", "Order", new { areas = "" });
            }

            try 
            {
                await this.orderService.DeleteUserOrderByOrderIdAsync(id);
                this.TempData[SuccessMessage] = $"{L[nameof(Order)].Value}   {L["CancelSuccessfully"].Value}"; //string.Format(CancelSuccessfully,nameof(Order));
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = $"{L["UnexpectedErrorTryingTo"].Value} {L["CancelOrder"].Value}"; //string.Format(UnexpectedErrorTryingTo, "Cancel Order");
            }
            return this.RedirectToAction("OrderStatus", "Order", new { area = "" });
        }     
    }
}
 
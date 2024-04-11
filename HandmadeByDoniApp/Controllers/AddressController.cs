using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.Infrastructure.Extensions;
using HandmadeByDoniApp.Web.ViewModels.Address;
using Microsoft.AspNetCore.Mvc;

using static HandmadeByDoniApp.Common.NotificationMessagesConstants;
using static HandmadeByDoniApp.Common.GeneralApplicationConstants;
using HandmadeByDoniApp.Web.ViewModels.Glass;

namespace HandmadeByDoniApp.Web.Controllers
{
    public class AddressController : BaseController
    {
        private readonly IOrderService orderService;
  
        private readonly IAddressService addressService;

        public AddressController(
            IOrderService orderService,
            IAddressService addressService)
        {
            this.orderService = orderService;
            this.addressService = addressService;

        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            AddressFormModel model = new AddressFormModel()
            {
                DeliveryCompanies = await this.addressService.AllDeliveryCompaniesAsync(),
                MethodPayments = await this.addressService.AllMethodPaymentsAsync(),

            };

            return this.View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddressFormModel formModel)
        {
            bool MethodPaymentIdExists = await this.addressService.MethodPaymentExistsByIdAsync(formModel.MethodPaymentId);

            if (MethodPaymentIdExists == false)
            {
                this.ModelState.AddModelError(nameof(formModel.MethodPaymentId), "Selected MethodPayment does not exist!");
            }
            bool DeliveryCompanyIdExists = await this.addressService.DeliveryCompanyExistsByIdAsync(formModel.DeliveryCompanyId);

            if (DeliveryCompanyIdExists == false)
            {
                this.ModelState.AddModelError(nameof(formModel.DeliveryCompanyId), "Selected DeliveryCompany does not exist!");
            }

            if (this.ModelState.IsValid == false)
            {
                formModel.DeliveryCompanies = await this.addressService.AllDeliveryCompaniesAsync();
                formModel.MethodPayments = await this.addressService.AllMethodPaymentsAsync();
                return this.View(formModel);
            }

            try
            {
                await this.addressService.CreateAddressAsync(formModel, User.GetId());
                this.TempData[SuccessMessage] = "Address was added successfully!";
            }
            catch (Exception)
            {
                formModel.DeliveryCompanies = await this.addressService.AllDeliveryCompaniesAsync();
                formModel.MethodPayments = await this.addressService.AllMethodPaymentsAsync();
                this.ModelState.AddModelError(string.Empty, UnexpectedError);
                this.TempData[ErrorMessage] = UnexpectedError;
                return View(formModel);
            }

            return this.RedirectToAction("DetailsOrder", "Order", new { area = "" });
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            string userId = User.GetId();
            bool isExists = await this.addressService.ExistsByUserIdAsync(userId);
            if (isExists == false)
            {
                TempData[ErrorMessage] = "You not have Address! You can add your address here";
                return RedirectToAction("Add", "Addres", new { area = "" });
            }

            try
            {
                AddressFormModel? formModel = 
                    await this.addressService
                              .GetAddressByUserIdAsync(userId);
                formModel.DeliveryCompanies = await this.addressService.AllDeliveryCompaniesAsync();
                formModel.MethodPayments = await this.addressService.AllMethodPaymentsAsync();
                return View(formModel);
            }
            catch (Exception)
            {
                return GeneralError();
            }

        }
        [HttpPost]
        public async Task<IActionResult> Edit(AddressFormModel formModel)
        {
            bool MethodPaymentIdExists = await this.addressService.MethodPaymentExistsByIdAsync(formModel.MethodPaymentId);

            if (MethodPaymentIdExists == false)
            {
                this.ModelState.AddModelError(nameof(formModel.MethodPaymentId), "Selected MethodPayment does not exist!");
            }
            bool DeliveryCompanyIdExists = await this.addressService.DeliveryCompanyExistsByIdAsync(formModel.DeliveryCompanyId);

            if (DeliveryCompanyIdExists == false)
            {
                this.ModelState.AddModelError(nameof(formModel.DeliveryCompanyId), "Selected DeliveryCompany does not exist!");
            }

            if (this.ModelState.IsValid == false)
            {
                formModel.DeliveryCompanies = await this.addressService.AllDeliveryCompaniesAsync();
                formModel.MethodPayments = await this.addressService.AllMethodPaymentsAsync();
                return this.View(formModel);
            }

            try
            {
                await this.addressService.CreateAddressAsync(formModel, User.GetId());
                this.TempData[SuccessMessage] = "Address was added successfully!";
            }
            catch (Exception)
            {
                formModel.DeliveryCompanies = await this.addressService.AllDeliveryCompaniesAsync();
                formModel.MethodPayments = await this.addressService.AllMethodPaymentsAsync();
                this.ModelState.AddModelError(string.Empty, UnexpectedError);
                this.TempData[ErrorMessage] = UnexpectedError;
                return View(formModel);
            }

            return this.RedirectToAction("DetailsOrder", "Order", new { area = "" });
        }
        private IActionResult GeneralError()
        {
            TempData[ErrorMessage] =
                "Unexpected error occurred! Please try again later";

            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}

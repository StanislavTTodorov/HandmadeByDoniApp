using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.Infrastructure.Extensions;
using HandmadeByDoniApp.Web.ViewModels.Address;
using Microsoft.AspNetCore.Mvc;
using HandmadeByDoniApp.Web.Resources;

using static HandmadeByDoniApp.Common.NotificationMessagesConstants;
using static HandmadeByDoniApp.Common.GeneralMessages;
using HandmadeByDoniApp.Data.Models;
using Microsoft.Extensions.Localization;

namespace HandmadeByDoniApp.Web.Controllers
{
    public class AddressController : BaseController<AddressController>
    {
  
        private readonly IAddressService addressService;

        public AddressController(
            IAddressService addressService)
        {
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
                this.ModelState.AddModelError(nameof(formModel.MethodPaymentId), PaymentMethodNotExist);
            }

            bool DeliveryCompanyIdExists = await this.addressService.DeliveryCompanyExistsByIdAsync(formModel.DeliveryCompanyId);

            if (DeliveryCompanyIdExists == false)
            {
                this.ModelState.AddModelError(nameof(formModel.DeliveryCompanyId), DeliveryCompanyNotExist);
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
                this.TempData[SuccessMessage] = $"{L[nameof(Address)]} {L["AddSuccessfully"]}";// string.Format(AddSuccessfully,nameof(Address));
            }
            catch (Exception)
            {
                formModel.DeliveryCompanies = await this.addressService.AllDeliveryCompaniesAsync();
                formModel.MethodPayments = await this.addressService.AllMethodPaymentsAsync();
                this.ModelState.AddModelError(string.Empty, string.Format(UnexpectedErrorTryingTo, $"add new {nameof(Address)}"));
                this.TempData[ErrorMessage] = $"{L["UnexpectedErrorTryingTo"]} {L["addNew"]} {L[$"{nameof(Address)}"]}";//string.Format(UnexpectedErrorTryingTo, $"add new {nameof(Address)}");
                return this.View(formModel);
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
                this.TempData[ErrorMessage] = L["NotHaveAddress"];// NotHaveAddress;
                return this.RedirectToAction("Add", "Addres", new { area = "" });
            }

            try
            {
                AddressFormModel? formModel = 
                    await this.addressService
                              .GetAddressByUserIdAsync(userId);

                if(formModel!=null) 
                {
                    formModel.DeliveryCompanies = await this.addressService.AllDeliveryCompaniesAsync();
                    formModel.MethodPayments = await this.addressService.AllMethodPaymentsAsync();
                }
                
                return this.View(formModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }

        }
        [HttpPost]
        public async Task<IActionResult> Edit(AddressFormModel formModel)
        {
            bool MethodPaymentIdExists = await this.addressService.MethodPaymentExistsByIdAsync(formModel.MethodPaymentId);

            if (MethodPaymentIdExists == false)
            {
                this.ModelState.AddModelError(nameof(formModel.MethodPaymentId), PaymentMethodNotExist);
            }

            bool DeliveryCompanyIdExists = await this.addressService.DeliveryCompanyExistsByIdAsync(formModel.DeliveryCompanyId);
            if (DeliveryCompanyIdExists == false)
            {
                this.ModelState.AddModelError(nameof(formModel.DeliveryCompanyId), DeliveryCompanyNotExist);
            }

            if (this.ModelState.IsValid == false)
            {
                formModel.DeliveryCompanies = await this.addressService.AllDeliveryCompaniesAsync();
                formModel.MethodPayments = await this.addressService.AllMethodPaymentsAsync();
                return this.View(formModel);
            }

            try
            {
                await this.addressService.EditAddressAsync(formModel, User.GetId());
                this.TempData[SuccessMessage] = $"{L[nameof(Address)]}   {L["EditSuccessfully"]}"; //string.Format(EditSuccessfully,nameof(Address));
            }
            catch (Exception)
            {
                formModel.DeliveryCompanies = await this.addressService.AllDeliveryCompaniesAsync();
                formModel.MethodPayments = await this.addressService.AllMethodPaymentsAsync();
                this.ModelState.AddModelError(string.Empty,string.Format(UnexpectedErrorTryingTo, $"edit the {nameof(Address)}"));
                this.TempData[ErrorMessage] = $"{L["UnexpectedErrorTryingTo"]} {L["editThe"]} {L[$"{nameof(Address)}"]}";// string.Format(UnexpectedErrorTryingTo, $"edit the {nameof(Address)}");
                return this.View(formModel);
            }

            return this.RedirectToAction("DetailsOrder", "Order", new { area = "" });
        }
        private IActionResult GeneralError()
        {
            this.TempData[ErrorMessage] = L["UnexpectedError"]; // UnexpectedError;

            return this.RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}

using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.Infrastructure.Extensions;
using HandmadeByDoniApp.Web.ViewModels.Address;
using Microsoft.AspNetCore.Mvc;
//using HandmadeByDoniApp.Web.Resources;

using static HandmadeByDoniApp.Common.NotificationMessagesConstants;
using static HandmadeByDoniApp.Common.GeneralMessages;
using HandmadeByDoniApp.Data.Models;
using Microsoft.Extensions.Localization;
using Resources.Resources;

namespace HandmadeByDoniApp.Web.Controllers
{
    public class AddressController : BaseController<AddressController>
    {
  
        private readonly IAddressService addressService;
        private IStringLocalizer<App> L;

        public AddressController(
            IAddressService addressService, IStringLocalizer<App> l)
        {
            this.addressService = addressService;
            L = l;
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
                this.TempData[SuccessMessage] = $"{L[nameof(Address)].Value}   {L["AddSuccessfully"].Value}";// string.Format(AddSuccessfully,nameof(Address));
            }
            catch (Exception)
            {
                formModel.DeliveryCompanies = await this.addressService.AllDeliveryCompaniesAsync();
                formModel.MethodPayments = await this.addressService.AllMethodPaymentsAsync();
                this.ModelState.AddModelError(string.Empty, string.Format(UnexpectedErrorTryingTo, $"add new {nameof(Address)}"));
                this.TempData[ErrorMessage] = $"{L["UnexpectedErrorTryingTo"].Value} {L["addNew"].Value} {L[$"{nameof(Address)}"].Value}";//string.Format(UnexpectedErrorTryingTo, $"add new {nameof(Address)}");
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
                this.TempData[ErrorMessage] = L["NotHaveAddress"].Value;// NotHaveAddress;
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
                this.TempData[SuccessMessage] = $"{L[nameof(Address)]}   {L["EditSuccessfully"].Value}"; //string.Format(EditSuccessfully,nameof(Address));
            }
            catch (Exception)
            {
                formModel.DeliveryCompanies = await this.addressService.AllDeliveryCompaniesAsync();
                formModel.MethodPayments = await this.addressService.AllMethodPaymentsAsync();
                this.ModelState.AddModelError(string.Empty,string.Format(UnexpectedErrorTryingTo, $"edit the {nameof(Address)}"));
                this.TempData[ErrorMessage] = $"{L["UnexpectedErrorTryingTo"].Value} {L["editThe"].Value} {L[$"{nameof(Address)}"].Value}";// string.Format(UnexpectedErrorTryingTo, $"edit the {nameof(Address)}");
                return this.View(formModel);
            }

            return this.RedirectToAction("DetailsOrder", "Order", new { area = "" });
        }
        private IActionResult GeneralError()
        {
            this.TempData[ErrorMessage] = L["UnexpectedError"].Value; // UnexpectedError;

            return this.RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}

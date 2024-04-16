
using HandmadeByDoniApp.Data.Models;
using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.ViewModels.Decanter;
using Microsoft.AspNetCore.Mvc;
using static HandmadeByDoniApp.Common.GeneralMessages;
using static HandmadeByDoniApp.Common.NotificationMessagesConstants;

namespace HandmadeByDoniApp.Web.Areas.Admin.Controllers
{
    public class DecanterController :BaseAdminController
    {
        private readonly IDecanterService decanterService;

        public DecanterController(IDecanterService decanterService)
        {
            this.decanterService = decanterService;
        }

        [HttpGet]
        public IActionResult Add()
        {
            DecanterFormModel model = new DecanterFormModel();

            return this.View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(DecanterFormModel formModel)
        {

            if (this.ModelState.IsValid == false)
            {
                return this.View(formModel);
            }

            try
            {
                await this.decanterService.CreateDecanterAsync(formModel);
                this.TempData[SuccessMessage] = string.Format(AddSuccessfully,nameof(Decanter));
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, string.Format(UnexpectedErrorTryingTo, $"add new {nameof(Decanter)}"));
                this.TempData[ErrorMessage] = string.Format(UnexpectedErrorTryingTo, $"add new {nameof(Decanter)}");
                return this.View(formModel);
            }

            return this.RedirectToAction("Index", "Home", new { area = "" });

        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            bool decanterExists = await this.decanterService
                .ExistsByIdAsync(id);
            if (decanterExists == false)
            {
                this.TempData[ErrorMessage] = string.Format(ProductNotExist,nameof(Decanter));
                return this.RedirectToAction("All", "Producr", new { area = "" });
            }

            try
            {
                DecanterFormModel formModel = await this.decanterService
                    .GetDecanterForEditByIdAsync(id);

                return this.View(formModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, DecanterFormModel formModel)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.View(formModel);
            }

            bool boxExists = await this.decanterService
                .ExistsByIdAsync(id);
            if (boxExists == false)
            {
                this.TempData[ErrorMessage] = string.Format(ProductNotExist,nameof(Decanter));
                return this.RedirectToAction("All", "Producr", new { area = "" });
            }

            try
            {
                await this.decanterService.EditDecanterByIdAndFormModelAsync(id, formModel);
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, string.Format(UnexpectedErrorTryingTo, $"edit the {nameof(Decanter)}"));

                return View(formModel);
            }

            this.TempData[SuccessMessage] = string.Format(UnexpectedErrorTryingTo, $"edit the {nameof(Decanter)}");
            return this.RedirectToAction("Details", "Decanter", new { area = "", id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id, string returnUrl)
        {
            bool isExist = await this.decanterService.ExistsByIdAsync(id);
            if (isExist == false)
            {
                this.TempData[ErrorMessage] = string.Format(ProductNotExist, nameof(Decanter));
                return this.Redirect(returnUrl);
            }

            try
            {
                await this.decanterService.SoftDeleteByIdAsync(id);
                this.TempData[SuccessMessage] = string.Format(DeleteSuccessfully,nameof(Decanter));
                return this.Redirect(returnUrl);
            }
            catch (Exception)
            {
                return this.GeneralError(returnUrl);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Recovery(string id, string returnUrl)
        {
            bool isExist = await this.decanterService.ExistsByIdAsync(id);
            if (isExist == false)
            {
                this.TempData[ErrorMessage] = string.Format(ProductNotExist, nameof(Decanter));
                return this.Redirect(returnUrl);
            }

            try
            {
                await this.decanterService.RecoveryByIdAsync(id);
                this.TempData[SuccessMessage] = string.Format(RecoverySuccessfully,nameof(Decanter));
                return this.Redirect(returnUrl);
            }
            catch (Exception)
            {
                return this.GeneralError(returnUrl);
            }
        }

        private IActionResult GeneralError(string? returnUrl = null)
        {
            this.TempData[ErrorMessage] = UnexpectedError;
               
            if (returnUrl == null)
            {
                return this.RedirectToAction("Index", "Home", new { area = "" });
            }

            return this.Redirect(returnUrl);
        }
    }
}

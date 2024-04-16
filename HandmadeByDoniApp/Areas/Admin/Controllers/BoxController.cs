using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.ViewModels.Box;
using Microsoft.AspNetCore.Mvc;
using static HandmadeByDoniApp.Common.NotificationMessagesConstants;
using static HandmadeByDoniApp.Common.GeneralMessages;
using HandmadeByDoniApp.Data.Models;



namespace HandmadeByDoniApp.Web.Areas.Admin.Controllers
{
    public class BoxController : BaseAdminController
    {
        private readonly IBoxService boxService;

        public BoxController(IBoxService boxService)
        {
            this.boxService = boxService;
        }

        [HttpGet]
        public IActionResult Add()
        {
            BoxFormModel model = new BoxFormModel();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(BoxFormModel formModel)
        {

            if (this.ModelState.IsValid == false)
            {
                return this.View(formModel);
            }

            try
            {
                await this.boxService.CreateBoxAsync(formModel);
                this.TempData[SuccessMessage] = string.Format(AddSuccessfully,nameof(Box));
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, string.Format(UnexpectedErrorTryingTo, $"add new {nameof(Box)}"));
                this.TempData[ErrorMessage] = string.Format(UnexpectedErrorTryingTo, $"add new {nameof(Box)}");
                return this.View(formModel);
            }

            return this.RedirectToAction("Index", "Home", new { area = "" });

        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            bool boxExists = await this.boxService
                .ExistsByIdAsync(id);
            if (boxExists==false)
            {
                this.TempData[ErrorMessage] = string.Format(ProductNotExist,nameof(Box));
                return this.RedirectToAction("All", "Producr", new { area = "" });
            }

            try
            {
                BoxFormModel formModel = await this.boxService
                    .GetBoxForEditByIdAsync(id);

                return this.View(formModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string id,BoxFormModel formModel)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.View(formModel);
            }

            bool boxExists = await this.boxService
                .ExistsByIdAsync(id);
            if (boxExists == false)
            {
                this.TempData[ErrorMessage] = string.Format(ProductNotExist, nameof(Box));
                return this.RedirectToAction("All", "Producr", new { area = "" });
            }

            try
            {
                await this.boxService.EditBoxByIdAndFormModelAsync(id, formModel);
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty,string.Format(UnexpectedErrorTryingTo, $"edit the {nameof(Box)}"));

                return this.View(formModel);
            }

            TempData[SuccessMessage] = string.Format(UnexpectedErrorTryingTo, $"edit the {nameof(Box)}");
            return this.RedirectToAction("Details", "Box", new { area = "", id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id,string returnUrl)
        {
            bool isBox = await this.boxService.ExistsByIdAsync(id);
            if (isBox==false)
            {
                TempData[ErrorMessage] = string.Format(ProductNotExist,nameof(Box));
                return this.Redirect(returnUrl);
            }

            try
            {
                await this.boxService.SoftDeleteByIdAsync(id);
                this.TempData[SuccessMessage] = string.Format(DeleteSuccessfully,nameof(Box));
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
            bool isBox = await this.boxService.ExistsByIdAsync(id);
            if (isBox == false)
            {
                this.TempData[ErrorMessage] = string.Format(ProductNotExist,nameof(Box));
                return this.Redirect(returnUrl);
            }

            try
            {
                await this.boxService.RecoveryByIdAsync(id);
                this.TempData[SuccessMessage] = string.Format(RecoverySuccessfully,nameof(Box));
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

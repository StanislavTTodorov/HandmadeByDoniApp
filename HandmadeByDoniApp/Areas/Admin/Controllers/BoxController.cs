using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.ViewModels.Box;
using Microsoft.AspNetCore.Mvc;
using static HandmadeByDoniApp.Common.NotificationMessagesConstants;
using static HandmadeByDoniApp.Common.GeneralApplicationConstants;



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
                TempData[SuccessMessage] = "Box was added successfully!";
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, UnexpectedError);
                this.TempData[ErrorMessage] = UnexpectedError;
                return View(formModel);
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
                TempData[ErrorMessage] = "Box with the provided id does not exist!";
                return RedirectToAction("All", "Producr", new { area = "" });
            }

            try
            {
                BoxFormModel formModel = await this.boxService
                    .GetBoxForEditByIdAsync(id);              

                return View(formModel);
            }
            catch (Exception)
            {
                return GeneralError();
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
                TempData[ErrorMessage] = "Box with the provided id does not exist!";
                return RedirectToAction("All", "Producr", new { area = "" });
            }

            try
            {
                await this.boxService.EditBoxByIdAndFormModelAsync(id, formModel);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty,
                    "Unexpected error occurred while trying to update the box. Please try again later");

                return View(formModel);
            }

            TempData[SuccessMessage] = "Box was edited successfully!";
            return RedirectToAction("Details", "Box", new { area = "", id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id,string returnUrl)
        {
            bool isBox = await this.boxService.ExistsByIdAsync(id);
            if (isBox==false)
            {
                TempData[ErrorMessage] = "Box with the provided id does not exist!";
                return this.Redirect(returnUrl);
            }
            try
            {
                await this.boxService.SoftDeleteByIdAsync(id);
                TempData[SuccessMessage] = "Box was delete successfully!";
                return this.Redirect(returnUrl);
            }
            catch (Exception)
            {
                return GeneralError(returnUrl);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Recovery(string id, string returnUrl)
        {
            bool isBox = await this.boxService.ExistsByIdAsync(id);
            if (isBox == false)
            {
                TempData[ErrorMessage] = "Box with the provided id does not exist!";
                return this.Redirect(returnUrl);
            }
            try
            {
                await this.boxService.RecoveryByIdAsync(id);
                TempData[SuccessMessage] = "Box was recovery successfully!";
                return this.Redirect(returnUrl);
            }
            catch (Exception)
            {
                return GeneralError(returnUrl);
            }
        }
        private IActionResult GeneralError(string? returnUrl = null)
        {
            TempData[ErrorMessage] =
                "Unexpected error occurred! Please try again later";
            if (returnUrl == null) 
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            return this.Redirect(returnUrl);

        }

    }
}

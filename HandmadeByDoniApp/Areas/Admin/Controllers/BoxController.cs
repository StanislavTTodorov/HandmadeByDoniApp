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
                    "Unexpected error occurred while trying to update the house. Please try again later");

                return View(formModel);
            }

            TempData[SuccessMessage] = "Box was edited successfully!";
            return RedirectToAction("Details", "Box", new { area = "", id });
        }
        private IActionResult GeneralError()
        {
            TempData[ErrorMessage] =
                "Unexpected error occurred! Please try again later";

            return RedirectToAction("Index", "Home", new { area = "" });
        }

    }
}

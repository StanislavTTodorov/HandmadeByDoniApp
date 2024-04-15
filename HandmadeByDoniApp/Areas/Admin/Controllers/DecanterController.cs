
using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.ViewModels.Box;
using HandmadeByDoniApp.Web.ViewModels.Decanter;
using Microsoft.AspNetCore.Mvc;
using static HandmadeByDoniApp.Common.GeneralApplicationConstants;
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
                TempData[SuccessMessage] = "Decanter was added successfully!";
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
            bool decanterExists = await this.decanterService
                .ExistsByIdAsync(id);
            if (decanterExists == false)
            {
                TempData[ErrorMessage] = "Decanter with the provided id does not exist!";
                return RedirectToAction("All", "Producr", new { area = "" });
            }

            try
            {
                DecanterFormModel formModel = await this.decanterService
                    .GetDecanterForEditByIdAsync(id);

                return View(formModel);
            }
            catch (Exception)
            {
                return GeneralError();
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
                TempData[ErrorMessage] = "Decanter with the provided id does not exist!";
                return RedirectToAction("All", "Producr", new { area = "" });
            }

            try
            {
                await this.decanterService.EditDecanterByIdAndFormModelAsync(id, formModel);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty,
                    "Unexpected error occurred while trying to update the decanter. Please try again later");

                return View(formModel);
            }

            TempData[SuccessMessage] = "Decanter was edited successfully!";
            return RedirectToAction("Details", "Decanter", new { area = "", id });
        }
        private IActionResult GeneralError()
        {
            TempData[ErrorMessage] =
                "Unexpected error occurred! Please try again later";

            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}

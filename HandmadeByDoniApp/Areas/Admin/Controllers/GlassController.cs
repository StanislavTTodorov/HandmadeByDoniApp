using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.ViewModels.Box;
using HandmadeByDoniApp.Web.ViewModels.Glass;
using Microsoft.AspNetCore.Mvc;
using static HandmadeByDoniApp.Common.GeneralApplicationConstants;
using static HandmadeByDoniApp.Common.NotificationMessagesConstants;

namespace HandmadeByDoniApp.Web.Areas.Admin.Controllers
{
    public class GlassController : BaseAdminController
    {
        private readonly IGlassService glassService;
        private readonly IGlassCategoryService glassCategoryServise;
        public GlassController(IGlassCategoryService glassCategoryServise,
                                IGlassService glassService)
        {
            this.glassService = glassService;
            this.glassCategoryServise = glassCategoryServise;
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            GlassFormModel model = new GlassFormModel()
            {
                Categories = await this.glassCategoryServise.AllGlassCategoriesAsync()
            };
            return this.View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(GlassFormModel formModel)
        {
            bool glassCategoryExists = await this.glassCategoryServise
                                                 .ExistsIdAsync(formModel.CategoryId);
           
            if (glassCategoryExists == false)
            {
                this.ModelState.AddModelError(nameof(formModel.CategoryId), "Selected category does not exist!");
            }

            if (this.ModelState.IsValid == false)
            {
                formModel.Categories = await this.glassCategoryServise
                                                 .AllGlassCategoriesAsync();
                return this.View(formModel);
            }

            try
            {
                await this.glassService.CreateGlassAsync(formModel);
                this.TempData[SuccessMessage] = "Glass was added successfully!";
            }
            catch (Exception)
            {
                formModel.Categories = await this.glassCategoryServise.AllGlassCategoriesAsync();
                this.ModelState.AddModelError(string.Empty, UnexpectedError);
                this.TempData[ErrorMessage] = UnexpectedError;
                return View(formModel);
            }

            return this.RedirectToAction("Index", "Home", new { area = "" });

        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            bool glassExists = await this.glassService
                .ExistsByIdAsync(id);
            if (glassExists == false)
            {
                TempData[ErrorMessage] = "Glass with the provided id does not exist!";
                return RedirectToAction("All", "Producr", new { area = "" });
            }

            try
            {
                GlassFormModel formModel = await this.glassService
                    .GetGlassForEditByIdAsync(id);
                formModel.Categories = await this.glassCategoryServise.AllGlassCategoriesAsync();
                return View(formModel);
            }
            catch (Exception)
            {
                return GeneralError();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string id, GlassFormModel formModel)
        {
            if (this.ModelState.IsValid == false)
            {
                formModel.Categories = await this.glassCategoryServise.AllGlassCategoriesAsync();
                return this.View(formModel);
            }

            bool glassExists = await this.glassService
                .ExistsByIdAsync(id);
            if (glassExists == false)
            {
                TempData[ErrorMessage] = "Glass with the provided id does not exist!";
                return RedirectToAction("All", "Producr", new { area = "" });
            }

            try
            {
                await this.glassService.EditGlassByIdAndFormModelAsync(id, formModel);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty,
                    "Unexpected error occurred while trying to update the glass. Please try again later");
                formModel.Categories = await this.glassCategoryServise.AllGlassCategoriesAsync();
                return View(formModel);
            }

            TempData[SuccessMessage] = "Glass was edited successfully!";
            return RedirectToAction("Details", "Glass", new { area = "", id });
        }
        private IActionResult GeneralError()
        {
            TempData[ErrorMessage] =
                "Unexpected error occurred! Please try again later";

            return RedirectToAction("Index", "Home", new { area = "" });
        }

    }
}

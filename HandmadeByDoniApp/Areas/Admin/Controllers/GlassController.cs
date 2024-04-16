using HandmadeByDoniApp.Data.Models;
using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.ViewModels.Glass;
using Microsoft.AspNetCore.Mvc;
using static HandmadeByDoniApp.Common.GeneralMessages;
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
                this.ModelState.AddModelError(nameof(formModel.CategoryId), CategoryNotExist);
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
                this.TempData[SuccessMessage] = string.Format(AddSuccessfully,nameof(Glass));
            }
            catch (Exception)
            {
                formModel.Categories = await this.glassCategoryServise.AllGlassCategoriesAsync();
                this.ModelState.AddModelError(string.Empty, string.Format(UnexpectedErrorTryingTo, $"add new {nameof(Glass)}"));
                this.TempData[ErrorMessage] = string.Format(UnexpectedErrorTryingTo, $"add new {nameof(Glass)}");
                return this.View(formModel);
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
                TempData[ErrorMessage] = string.Format(ProductNotExist,nameof(Glass));
                return this.RedirectToAction("All", "Producr", new { area = "" });
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
                this.TempData[ErrorMessage] = string.Format(ProductNotExist, nameof(Glass));
                return this.RedirectToAction("All", "Producr", new { area = "" });
            }

            try
            {
                await this.glassService.EditGlassByIdAndFormModelAsync(id, formModel);
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, string.Format(UnexpectedErrorTryingTo, $"edit the {nameof(Glass)}"));
                formModel.Categories = await this.glassCategoryServise.AllGlassCategoriesAsync();

                return this.View(formModel);
            }

            this.TempData[SuccessMessage] = string.Format(UnexpectedErrorTryingTo, $"edit the {nameof(Glass)}");
            return this.RedirectToAction("Details", "Glass", new { area = "", id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id, string returnUrl)
        {
            bool isExist = await this.glassService.ExistsByIdAsync(id);
            if (isExist == false)
            {
                this.TempData[ErrorMessage] = string.Format(ProductNotExist, nameof(Glass));
                return this.Redirect(returnUrl);
            }
            try
            {
                await this.glassService.SoftDeleteByIdAsync(id);
                this.TempData[SuccessMessage] = string.Format(DeleteSuccessfully, nameof(Glass));
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
            bool isExist = await this.glassService.ExistsByIdAsync(id);
            if (isExist == false)
            {
                this.TempData[ErrorMessage] = string.Format(ProductNotExist, nameof(Glass));
                return this.Redirect(returnUrl);
            }
            try
            {
                await this.glassService.RecoveryByIdAsync(id);
                this.TempData[SuccessMessage] = string.Format(RecoverySuccessfully, nameof(Glass));
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

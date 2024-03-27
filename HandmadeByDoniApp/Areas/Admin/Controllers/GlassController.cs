using HandmadeByDoniApp.Services.Data.Interfaces;
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
    }
}

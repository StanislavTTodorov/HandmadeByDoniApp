using HandmadeByDoniApp.Data.Models;
using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.ViewModels.Glass;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using static HandmadeByDoniApp.Common.GeneralApplicationConstants;

namespace HandmadeByDoniApp.Web.Controllers
{
    // TODO Authorize only for the Admin

    public class GlassController : Controller
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
            // TODO Authorize only for the Admin
            GlassFormModel model = new GlassFormModel()
            {
                Categories = await this.glassCategoryServise.AllGlassCategoriesAsync()              
            };
            return this.View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(GlassFormModel formModel)
        {
            // TODO Authorize only for the Admin
            bool glassCategoryExists = await this.glassCategoryServise
                                                 .ExistsIdAsync(formModel.CategoryId);

            if (glassCategoryExists==false)
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
            }
            catch (Exception)
            {
                formModel.Categories = await this.glassCategoryServise.AllGlassCategoriesAsync();
                this.ModelState.AddModelError(string.Empty,UnexpectedError);
                return View(formModel);
            }

            return this.RedirectToAction("Index", "Home");

        }
    }
}

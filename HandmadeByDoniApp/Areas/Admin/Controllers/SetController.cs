using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.ViewModels.Decanter;
using HandmadeByDoniApp.Web.ViewModels.Glass;
using HandmadeByDoniApp.Web.ViewModels.Set;
using Microsoft.AspNetCore.Mvc;
using static HandmadeByDoniApp.Common.GeneralApplicationConstants;
using static HandmadeByDoniApp.Common.NotificationMessagesConstants;

namespace HandmadeByDoniApp.Web.Areas.Admin.Controllers
{
    public class SetController : BaseAdminController
    {
        private readonly ISetService setService;
        private readonly IGlassCategoryService glassCategoryService;
        private readonly IGlassService glassService;
        private readonly IDecanterService decanterService;

        public SetController(ISetService setService,
                             IGlassCategoryService glassCategoryService,
                             IGlassService glassService,
                             IDecanterService decanterService)
        {
            this.setService = setService;
            this.glassCategoryService = glassCategoryService;
            this.glassService = glassService;
            this.decanterService = decanterService;

        }


        [HttpGet]
        public async Task<IActionResult> Add(bool on, int numberOfCups)
        {
            SetFormModel model = new SetFormModel()
            {
                NumberOfCups = numberOfCups,
                GlassOne = new GlassFormModel()
                {
                    Categories = await this.glassCategoryService.AllGlassCategoriesAsync()
                },
                GlassTwo = new GlassFormModel()
                {
                    Categories = await this.glassCategoryService.AllGlassCategoriesAsync()
                },
                GlassThree = null,
                GlassFour = null,
                Decanter = null

            };
            if (on)
            {
                model.Decanter = new DecanterFormModel();
            }
            if (model.NumberOfCups == 4)
            {
                model.GlassThree = new GlassFormModel()
                {
                    Categories = await this.glassCategoryService.AllGlassCategoriesAsync()
                };
                model.GlassFour = new GlassFormModel()
                {
                    Categories = await this.glassCategoryService.AllGlassCategoriesAsync()
                };
            }


            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(SetFormModel model)
        {
            bool glassOneCategoryExists = await this.glassCategoryService
                                                 .ExistsIdAsync(model.GlassOne.CategoryId);

            if (glassOneCategoryExists == false)
            {
                this.ModelState.AddModelError(nameof(model.GlassOne.CategoryId), "Selected category does not exist!");
            }

            bool glassTwoCategoryExists = await this.glassCategoryService
                                               .ExistsIdAsync(model.GlassTwo.CategoryId);

            if (glassTwoCategoryExists == false)
            {
                this.ModelState.AddModelError(nameof(model.GlassTwo.CategoryId), "Selected category does not exist!");
            }

            if (this.ModelState.IsValid == false)
            {
                model.GlassOne = new GlassFormModel()
                {
                    Categories = await this.glassCategoryService.AllGlassCategoriesAsync()
                };
                model.GlassTwo = new GlassFormModel()
                {
                    Categories = await this.glassCategoryService.AllGlassCategoriesAsync()
                };

                return this.View(model);
            }

            try
            {
                await this.setService.CreateSetAsync(model);
                this.TempData[SuccessMessage] = "Set was added successfully!";
            }
            catch (Exception)
            {
                model.GlassOne = new GlassFormModel()
                {
                    Categories = await this.glassCategoryService.AllGlassCategoriesAsync()
                };
                model.GlassTwo = new GlassFormModel()
                {
                    Categories = await this.glassCategoryService.AllGlassCategoriesAsync()
                };
                this.ModelState.AddModelError(string.Empty, UnexpectedError);
                this.TempData[ErrorMessage] = UnexpectedError;
                return View(model);
            }
            return this.RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}

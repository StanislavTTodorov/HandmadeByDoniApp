using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.ViewModels.Decanter;
using HandmadeByDoniApp.Web.ViewModels.Glass;
using HandmadeByDoniApp.Web.ViewModels.Set;
using Microsoft.AspNetCore.Mvc;
using static HandmadeByDoniApp.Common.GeneralMessages;
using static HandmadeByDoniApp.Common.NotificationMessagesConstants;
using HandmadeByDoniApp.Data.Models;


namespace HandmadeByDoniApp.Web.Areas.Admin.Controllers
{
    public class SetController : BaseAdminController
    {
        private readonly ISetService setService;
        private readonly IGlassCategoryService glassCategoryService;

        public SetController(ISetService setService,
                             IGlassCategoryService glassCategoryService,
                             IGlassService glassService,
                             IDecanterService decanterService)
        {
            this.setService = setService;
            this.glassCategoryService = glassCategoryService;
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


            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(SetFormModel model)
        {
            bool glassOneCategoryExists = await this.glassCategoryService
                                                 .ExistsIdAsync(model.GlassOne.CategoryId);

            if (glassOneCategoryExists == false)
            {
                this.ModelState.AddModelError(nameof(model.GlassOne.CategoryId), CategoryNotExist);
            }

            bool glassTwoCategoryExists = await this.glassCategoryService
                                               .ExistsIdAsync(model.GlassTwo.CategoryId);

            if (glassTwoCategoryExists == false)
            {
                this.ModelState.AddModelError(nameof(model.GlassTwo.CategoryId), CategoryNotExist);
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
                this.TempData[SuccessMessage] = string.Format(AddSuccessfully,nameof(Set));
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

                this.ModelState.AddModelError(string.Empty, string.Format(UnexpectedErrorTryingTo, $"add new {nameof(Set)}"));
                this.TempData[ErrorMessage] = string.Format(UnexpectedErrorTryingTo, $"add new {nameof(Set)}");

                return View(model);
            }
            return this.RedirectToAction("Index", "Home", new { area = "" });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            bool isExists = await this.setService
                .ExistsByIdAsync(id);
            if (isExists == false)
            {
                this.TempData[ErrorMessage] = string.Format(ProductNotExist,nameof(Set));
                return this.RedirectToAction("All", "Producr", new { area = "" });
            }

            try
            {
                OnlySetFormModel formModel = await this.setService.GetSetForEditByIdAsync(id);
              
                return this.View(formModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string id, OnlySetFormModel formModel)
        {
            if (this.ModelState.IsValid == false)
            {
                
                return this.View(formModel);
            }

            bool isExists = await this.setService
                .ExistsByIdAsync(id);
            if (isExists == false)
            {
                this.TempData[ErrorMessage] = string.Format(ProductNotExist, nameof(Set));
                return this.RedirectToAction("All", "Producr", new { area = "" });
            }

            try
            {
                await this.setService.EditSetByIdAndFormModelAsync(id, formModel);
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, string.Format(UnexpectedErrorTryingTo, $"edit the {nameof(Set)}"));
                
                return this.View(formModel);
            }

            TempData[SuccessMessage] = string.Format(AddSuccessfully,nameof(Set));
            return this.RedirectToAction("Details", "Set", new { area = "", id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id, string returnUrl)
        {
            bool isExist = await this.setService.ExistsByIdAsync(id);
            if (isExist == false)
            {
                this.TempData[ErrorMessage] = string.Format(ProductNotExist, nameof(Set));
                return this.Redirect(returnUrl);
            }

            try
            {
                await this.setService.SoftDeleteByIdAsync(id);
                this.TempData[SuccessMessage] = string.Format(DeleteSuccessfully,nameof(Set));
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
            bool isExist = await this.setService.ExistsByIdAsync(id);
            if (isExist == false)
            {
                this.TempData[ErrorMessage] = string.Format(ProductNotExist, nameof(Set));
                return this.Redirect(returnUrl);
            }

            try
            {
                await this.setService.RecoveryByIdAsync(id);
                this.TempData[SuccessMessage] = string.Format(RecoverySuccessfully,nameof(Set));
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

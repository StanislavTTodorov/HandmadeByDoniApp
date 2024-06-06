using HandmadeByDoniApp.Services.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static HandmadeByDoniApp.Common.NotificationMessagesConstants;
using static HandmadeByDoniApp.Common.GeneralMessages;
using HandmadeByDoniApp.Web.ViewModels.Product;
using HandmadeByDoniApp.Data.Models;

namespace HandmadeByDoniApp.Web.Areas.Admin.Controllers
{
    public class ProductController :BaseAdminController
    {
        //private readonly IGlassService glassService;
        //private readonly IDecanterService decanterService;
        //private readonly IBoxService boxService;
        //private readonly ISetService setService;
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;

        public ProductController(
                                 //IGlassService glassService,
                                // IDecanterService decanterService,
                                 //IBoxService boxService,
                                 //ISetService setService
                                 IProductService productService,
                                 ICategoryService categoryService)
        {
            //this.glassService = glassService;
            //this.decanterService = decanterService;
            //this.boxService = boxService;
            //this.setService = setService;
            this.productService = productService;
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            ProductFormModel model = new ProductFormModel()
            {
                Categories = await this.categoryService.AllCategoriesAsync()
            };
        
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductFormModel formModel)
        {
            bool categoryExists = await this.categoryService.ExistsIdAsync(formModel.CategoryId);

            if (categoryExists == false)
            {
                this.ModelState.AddModelError(nameof(formModel.CategoryId), CategoryNotExist);
            }

            if (this.ModelState.IsValid == false)
            {
                formModel.Categories = await this.categoryService
                                                 .AllCategoriesAsync();
                return this.View(formModel);
            }

            try
            {
                await this.productService.CreateProductAsync(formModel);
                this.TempData[SuccessMessage] = string.Format(AddSuccessfully, nameof(Product));
            }
            catch (Exception)
            {
                formModel.Categories = await this.categoryService.AllCategoriesAsync();
                this.ModelState.AddModelError(string.Empty, string.Format(UnexpectedErrorTryingTo, $"add new {nameof(Glass)}"));
                this.TempData[ErrorMessage] = string.Format(UnexpectedErrorTryingTo, $"add new {nameof(Glass)}");
                return this.View(formModel);
            }

            return this.RedirectToAction("Index", "Home", new { area = "" });


        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            //bool isGlass = await this.glassService.ExistsByIdAsync(id);
            //if (isGlass)
            //{
            //    return this.RedirectToAction("Edit", "Glass", new { id });
            //}

            //bool isDecanter = await this.decanterService.ExistsByIdAsync(id);
            //if (isDecanter)
            //{
            //    return this.RedirectToAction("Edit", "Decanter", new { id });
            //}

            //bool isBox = await this.boxService.ExistsByIdAsync(id);
            //if (isBox)
            //{
            //    return this.RedirectToAction("Edit", "Box", new { id });
            //}

            //bool isSet = await this.setService.ExistsByIdAsync(id);
            //if (isSet)
            //{
            //    return this.RedirectToAction("Edit", "Set", new { id });
            //}
            bool isProduct = await this.productService.ExistsByIdAsync(id);
            if (isProduct)
            {
                return this.RedirectToAction("Edit", "Set", new { id });
            }

            this.TempData[ErrorMessage] = ProductNotExistChooseFrom;
            return this.RedirectToAction("All", "Product", new { area = "" });

        }
        [HttpGet]
        public async Task<IActionResult> Delete(string id, string returnUrl)
        {
            //bool isGlass = await this.glassService.ExistsByIdAsync(id);
            //if (isGlass)
            //{
            //    return this.RedirectToAction("Delete", "Glass", new { id,returnUrl });
            //}

            //bool isDecanter = await this.decanterService.ExistsByIdAsync(id);
            //if (isDecanter)
            //{
            //    return this.RedirectToAction("Delete", "Decanter", new { id, returnUrl });
            //}

            //bool isBox = await this.boxService.ExistsByIdAsync(id);
            //if (isBox)
            //{
            //    return this.RedirectToAction("Delete", "Box", new { id, returnUrl });
            //}

            //bool isSet = await this.setService.ExistsByIdAsync(id);
            //if (isSet)
            //{
            //    return this.RedirectToAction("Delete", "Set", new { id, returnUrl });
            //}

            this.TempData[ErrorMessage] = ProductNotExistChooseFrom;
            return this.RedirectToAction("All", "Product", new { area = "" });

        }
        [HttpGet]
        public async Task<IActionResult> Recovery(string id, string returnUrl)
        {
            //bool isGlass = await this.glassService.ExistsByIdAsync(id);
            //if (isGlass)
            //{
            //    return this.RedirectToAction("Recovery", "Glass", new { id, returnUrl });
            //}

            //bool isDecanter = await this.decanterService.ExistsByIdAsync(id);
            //if (isDecanter)
            //{
            //    return this.RedirectToAction("Recovery", "Decanter", new { id, returnUrl });
            //}

            //bool isBox = await this.boxService.ExistsByIdAsync(id);
            //if (isBox)
            //{
            //    return this.RedirectToAction("Recovery", "Box", new { id, returnUrl });
            //}

            //bool isSet = await this.setService.ExistsByIdAsync(id);
            //if (isSet)
            //{
            //    return this.RedirectToAction("Recovery", "Set", new { id, returnUrl });
            //}

            this.TempData[ErrorMessage] = ProductNotExistChooseFrom;
            return this.Redirect(returnUrl);

        }
    }
}

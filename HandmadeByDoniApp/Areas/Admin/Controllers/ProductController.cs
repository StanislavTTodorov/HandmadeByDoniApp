﻿using HandmadeByDoniApp.Services.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static HandmadeByDoniApp.Common.NotificationMessagesConstants;
using static HandmadeByDoniApp.Common.GeneralMessages;
using HandmadeByDoniApp.Web.ViewModels.Product;
using HandmadeByDoniApp.Data.Models;
using HandmadeByDoniApp.Web.ViewModels.Glass;

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
                this.ModelState.AddModelError(string.Empty, string.Format(UnexpectedErrorTryingTo, $"add new {nameof(Product)}"));
                this.TempData[ErrorMessage] = string.Format(UnexpectedErrorTryingTo, $"add new {nameof(Product)}");
                return this.View(formModel);
            }

            return this.RedirectToAction("Index", "Home", new { area = "" });


        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            bool productExists = await this.productService
                .ExistsByIdAsync(id);
            if (productExists == false)
            {
                TempData[ErrorMessage] = string.Format(ProductNotExist, nameof(Product));
                return this.RedirectToAction("All", "Producr", new { area = "" });
            }

            try
            {
                ProductFormModel formModel = await this.productService
                    .GetProductForEditByIdAsync(id);
                formModel.Categories = await this.categoryService.AllCategoriesAsync();
                return View(formModel);
            }
            catch (Exception)
            {
                return GeneralError();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, ProductFormModel formModel)
        {
            if (this.ModelState.IsValid == false)
            {
                formModel.Categories = await this.categoryService.AllCategoriesAsync();
                return this.View(formModel);
            }

            bool productExists = await this.productService
                .ExistsByIdAsync(id);
            if (productExists == false)
            {
                this.TempData[ErrorMessage] = string.Format(ProductNotExist, nameof(Product));
                return this.RedirectToAction("All", "Producr", new { area = "" });
            }

            try
            {
                await this.productService.EditProductByIdAndFormModelAsync(id, formModel);
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, string.Format(UnexpectedErrorTryingTo, $"edit the {nameof(Product)}"));
                formModel.Categories = await this.categoryService.AllCategoriesAsync();

                return this.View(formModel);
            }

            this.TempData[SuccessMessage] = string.Format(UnexpectedErrorTryingTo, $"edit the {nameof(Product)}");
            return this.RedirectToAction("Details", "Product", new { area = "", id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id, string returnUrl)
        {
            bool isExist = await this.productService.ExistsByIdAsync(id);
            if (isExist == false)
            {
                this.TempData[ErrorMessage] = string.Format(ProductNotExist, nameof(Glass));
                return this.Redirect(returnUrl);
            }
            try
            {
                await this.productService.SoftDeleteByIdAsync(id);
                this.TempData[SuccessMessage] = string.Format(DeleteSuccessfully, nameof(productService));
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
            bool isExist = await this.productService.ExistsByIdAsync(id);
            if (isExist == false)
            {
                this.TempData[ErrorMessage] = string.Format(ProductNotExist, nameof(Product));
                return this.Redirect(returnUrl);
            }
            try
            {
                await this.productService.RecoveryByIdAsync(id);
                this.TempData[SuccessMessage] = string.Format(RecoverySuccessfully, nameof(Product));
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

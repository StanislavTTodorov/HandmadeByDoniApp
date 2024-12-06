using HandmadeByDoniApp.Services.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static HandmadeByDoniApp.Common.NotificationMessagesConstants;
using static HandmadeByDoniApp.Common.GeneralMessages;
using HandmadeByDoniApp.Web.ViewModels.Product;
using HandmadeByDoniApp.Data.Models;

namespace HandmadeByDoniApp.Web.Areas.Admin.Controllers
{
    public class ProductController :BaseAdminController<ProductController>
    {      
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;

        public ProductController(IProductService productService,
                                 ICategoryService categoryService)
        {
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
                return await this.ModelStateNotValid(formModel);
            }

            try
            {
                //Запис на снимка 
                formModel.ImageUrl = await UploadImage(formModel.Image, formModel.Title);
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
               return await this.ModelStateNotValid(formModel);
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
                this.TempData[ErrorMessage] = string.Format(ProductNotExist, nameof(Product));
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
        public async Task<string> UploadImage(IFormFile imageFile,string name)
        {

            // Определете пътя за съхранение
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            Directory.CreateDirectory(uploadPath); // Създава папката, ако не съществува

            // Уникално име за файла
            var fileName = $"{name}_{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
            var filePath = Path.Combine(uploadPath, fileName);

            // Запазване на файла
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return $"/uploads/{fileName}";
        }
        private async  Task<IActionResult> ModelStateNotValid(ProductFormModel formModel)
        {
            formModel.Categories = await this.categoryService.AllCategoriesAsync();
            return this.View(formModel);
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

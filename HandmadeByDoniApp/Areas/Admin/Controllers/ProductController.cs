﻿using HandmadeByDoniApp.Services.Data.Interfaces;
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
        private ILogger logger;

        public ProductController(IProductService productService,
                                 ICategoryService categoryService,
                                 ILogger<ProductController> logger)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.logger = logger;
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
                formModel.ImageUrls = await UploadImage(formModel);
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
                if(string.IsNullOrEmpty(formModel.ImageUrls))
                {
                    formModel.ImageUrls = await UploadImage(formModel);
                }
                else
                {
                    formModel.ImageUrls +=  "," + await UploadImage(formModel);
                }
                await DeleteUnnecessaryImage(formModel);
                
                await this.productService.EditProductByIdAndFormModelAsync(id, formModel);
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, string.Format(UnexpectedErrorTryingTo, $"edit the {nameof(Product)}"));
                formModel.Categories = await this.categoryService.AllCategoriesAsync();

                return this.View(formModel);
            }

            this.TempData[SuccessMessage] = string.Format(EditSuccessfully,$"{nameof(Product)}");
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
        private async Task DeleteUnnecessaryImage(ProductFormModel formModel)
        {
            ProductFormModel exformModel = await this.productService.GetProductForEditByIdAsync(formModel.Id!);
            if (exformModel != null && exformModel.ImageUrls != null)
            {
                List<string> exImeges = exformModel.ImageUrls.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList();
                //List<string> newImeges = formModel.ImageUrls.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList();
                foreach (string eximg in exImeges)
                {
                    if (formModel.ImageUrls!=null&&!formModel.ImageUrls.Contains(eximg))
                    {
                        // Извличане на физическия път до изображението
                        string fileToDelete = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", eximg.TrimStart('/'));

                        if (System.IO.File.Exists(fileToDelete))
                        {
                            System.IO.File.Delete(fileToDelete);
                            logger.LogWarning($"Изтрито изображение: {fileToDelete}");
                        }
                        else
                        {
                            logger.LogWarning($"Файлът не съществува: {fileToDelete}");
                        }
                    }
                }
            }           
        }
        public async Task<string> UploadImage(ProductFormModel formModel)
        {
            logger.LogWarning("В метод UploadImage");
            // Определете пътя за съхранение
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            logger.LogWarning("Определен е пътя за съхранение");
            Directory.CreateDirectory(uploadPath); // Създава папката, ако не съществува
            //string Date = DateTime.Now.ToShortDateString().Replace(" г.", string.Empty);
            List<string> imageUrls = new List<string>();
            logger.LogWarning($"{uploadPath} И е Зъздаден нов imageUrls");
            foreach (var imageFile in formModel.Images)
            {
                 // Уникално име за файла с разширение
                 //string fileName = $"{formModel.Title}_{Date}_{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
                string fileName = $"{formModel.Title.Replace(" ","_")}_{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
                logger.LogWarning($"fileName = {fileName}");
                string filePath = Path.Combine(uploadPath, fileName);
                logger.LogWarning($"filePath = {filePath}");
                // Запазване на файла
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                    logger.LogWarning($"Запазване на файла");
                }

                // Добавяне на URL към списъка
                imageUrls.Add($"/uploads/{fileName}");
            }

            logger.LogWarning("финал на UploadImage");
            return string.Join(",", imageUrls);
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

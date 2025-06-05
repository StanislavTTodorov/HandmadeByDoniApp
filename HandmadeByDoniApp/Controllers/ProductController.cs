using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Servises.Data.Models.Product;
using HandmadeByDoniApp.Web.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static HandmadeByDoniApp.Common.NotificationMessagesConstants;
using static HandmadeByDoniApp.Common.GeneralMessages;
using Ganss.Xss;
using HandmadeByDoniApp.Web.Infrastructure.Extensions;
using HandmadeByDoniApp.Web.ViewModels.Comment;
using HandmadeByDoniApp.Data.Models;
//using HandmadeByDoniApp.Web.Resources;
using Microsoft.Extensions.Localization;
using Resources.Resources;



namespace HandmadeByDoniApp.Web.Controllers
{
    public class ProductController : BaseController<ProductController>
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        private IStringLocalizer<App> L;

        public ProductController(IProductService productService,
                                 ICategoryService categoryService,
                                  IStringLocalizer<App> l)
        {
         
            this.productService = productService;
            this.categoryService = categoryService;
            this.L = l;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery] AllProductsQueryModel queryModel)
        {
            AllProductFilteredAndPagedServiceModel serviceModel = await this.productService.AllProductsAsync(queryModel);

            queryModel.Products =serviceModel.Products;
            queryModel.TotalProduct = serviceModel.TotalProductCount;
            queryModel.GlassCategories = await this.categoryService.AllCategoryNameAsync();

            return this.View(queryModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string id)
        {
            bool isProduct = await this.productService.ExistsByIdAsync(id);
            if (isProduct == false)
            {
                this.TempData[ErrorMessage] = $"{L[nameof(Product)].Value}     {L["ProductNotExist"].Value}"; //string.Format(ProductNotExist, nameof(Product)); ;
                return this.RedirectToAction("All", "Product");
            }

            try
            {
                ProductViewModel viewModel = await this.productService.GetProductDetailsByIdAsync(id);
                return this.View(viewModel);
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = L["UnexpectedError"].Value;// UnexpectedError;
                return this.RedirectToAction("All", "Product");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Comment(string id)
        {
            bool isProduct = await this.productService.ExistsByIdAsync(id);
            if (isProduct == false)
            {
                this.TempData[ErrorMessage] = $"{L[nameof(Product)]} {L["ProductNotExist"].Value}";// string.Format(ProductNotExist, nameof(Product)); ;
                return this.RedirectToAction("All", "Pcoduct");
            }

            try
            {
                ProductCommentViewModel viewModel = await this.productService.GetProductCommentByIdAsync(id);
                return this.View("~/Views/Comment/Comment.cshtml", viewModel);
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = L["UnexpectedError"].Value; //UnexpectedError;
                return this.RedirectToAction("All", "Product");
            }

        }

        [HttpGet]
        public async Task<IActionResult> WriteComment(string id)
        {
            bool isProduct = await this.productService.ExistsByIdAsync(id);
            if (isProduct == false)
            {
                this.TempData[ErrorMessage] = $"{L[nameof(Product)]} {L["ProductNotExist"].Value}"; //string.Format(ProductNotExist, nameof(Product)); ;
                return this.RedirectToAction("All", "Pcoduct");
            }

            CommentFormModel model = new CommentFormModel();

            return this.View("~/Views/Comment/WriteToComment.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> WriteComment(string id, CommentFormModel formModel)
        {
            bool isProduct = await this.productService.ExistsByIdAsync(id);
            if (isProduct == false)
            {
                this.TempData[ErrorMessage] = $"{L[nameof(Product)].Value} {L["ProductNotExist"].Value}"; //string.Format(ProductNotExist, nameof(Product)); ;
                return this.RedirectToAction("All", "Pcoduct");
            }

            var sanitizer = new HtmlSanitizer();
            formModel.Text = sanitizer.Sanitize(formModel.Text);

            if (this.ModelState.IsValid == false)
            {
                return this.View("~/Views/Comment/WriteToComment.cshtml", formModel);
            }

            try
            {
                string userId = User.GetId();
                await this.productService.CreateCommentByUserIdAndByProductIdAsync(userId!, formModel, id);
                this.TempData[SuccessMessage] = $"{L[nameof(Comment)].Value} {L["AddSuccessfully"].Value}"; //string.Format(AddSuccessfully, nameof(Comment)); ;
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, string.Format(UnexpectedErrorTryingTo, $"add new {nameof(Comment)}"));
                this.TempData[ErrorMessage] = $"{L["UnexpectedErrorTryingTo"].Value} {L["addNew"].Value} {L[nameof(Comment)].Value}"; // string.Format(UnexpectedErrorTryingTo, $"add new {nameof(Comment)}");
            }

            return this.RedirectToAction("Comment", "Product", new { id });
        }
    }
}

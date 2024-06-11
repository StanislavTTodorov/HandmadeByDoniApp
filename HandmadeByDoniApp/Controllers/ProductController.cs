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
using HandmadeByDoniApp.Web.ViewModels.Glass;
using HandmadeByDoniApp.Data.Models;



namespace HandmadeByDoniApp.Web.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IGlassService glassService;
        private readonly IDecanterService decanterService;
        private readonly IBoxService boxService;
        private readonly ISetService setService;
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;

        public ProductController(IGlassService glassService,
                                 IDecanterService decanterService,
                                 IBoxService boxService,
                                 ISetService setService,
                                 IProductService productService,
                                 ICategoryService categoryService)
        {
            this.glassService = glassService;
            this.decanterService = decanterService;
            this.boxService = boxService;
            this.setService = setService;
            this.productService = productService;
            this.categoryService = categoryService;
        }

        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<IActionResult> Details(string id)
        //{
        //    bool isProduct = await this.glassService.ExistsByIdAsync(id);
        //    if(isProduct)
        //    {
        //        return this.RedirectToAction("Details", "Glass", new { id });
        //    }

        //    bool isDecanter = await this.decanterService.ExistsByIdAsync(id);
        //    if(isDecanter)
        //    {
        //        return this.RedirectToAction("Details","Decanter",new { id });
        //    }

        //    bool isBox =  await this.boxService.ExistsByIdAsync(id);
        //    if(isBox)
        //    {
        //        return this.RedirectToAction("Details", "Box", new { id });
        //    }

        //    bool isSet = await this.setService.ExistsByIdAsync(id);
        //    if(isSet)
        //    {
        //        return this.RedirectToAction("Details", "Set", new { id });
        //    }

        //    this.TempData[ErrorMessage] = ProductNotExistChooseFrom;
        //    return this.RedirectToAction("All", "Product");
        //}

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
                this.TempData[ErrorMessage] = string.Format(ProductNotExist, nameof(Product)); ;
                return this.RedirectToAction("All", "Product");
            }

            try
            {
                ProductViewModel viewModel = await this.productService.GetProductDetailsByIdAsync(id);
                return this.View(viewModel);
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = UnexpectedError;
                return this.RedirectToAction("All", "Product");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Comment(string id)
        {
            bool isProduct = await this.productService.ExistsByIdAsync(id);
            if (isProduct == false)
            {
                this.TempData[ErrorMessage] = string.Format(ProductNotExist, nameof(Product)); ;
                return this.RedirectToAction("All", "Pcoduct");
            }

            try
            {
                ProductCommentViewModel viewModel = await this.productService.GetProductCommentByIdAsync(id);
                return this.View("~/Views/Comment/Comment.cshtml", viewModel);
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = UnexpectedError;
                return this.RedirectToAction("All", "Product");
            }

        }

        [HttpGet]
        public async Task<IActionResult> WriteComment(string id)
        {
            bool isProduct = await this.productService.ExistsByIdAsync(id);
            if (isProduct == false)
            {
                this.TempData[ErrorMessage] = string.Format(ProductNotExist, nameof(Product)); ;
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
                this.TempData[ErrorMessage] = string.Format(ProductNotExist, nameof(Glass)); ;
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
                this.TempData[SuccessMessage] = string.Format(AddSuccessfully, nameof(Comment)); ;
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, string.Format(UnexpectedErrorTryingTo, $"add new {nameof(Comment)}"));
                this.TempData[ErrorMessage] = string.Format(UnexpectedErrorTryingTo, $"add new {nameof(Comment)}");
            }

            return this.RedirectToAction("Comment", "Product", new { id });
        }
    }
}

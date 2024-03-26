using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Servises.Data.Models.Product;
using HandmadeByDoniApp.Web.ViewModels.Comment;
using HandmadeByDoniApp.Web.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static HandmadeByDoniApp.Common.NotificationMessagesConstants;
using static HandmadeByDoniApp.Common.GeneralApplicationConstants;
using HandmadeByDoniApp.Web.Infrastructure.Extensions;


namespace HandmadeByDoniApp.Web.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IGlassService glassService;
        private readonly IDecanterService decanterService;
        private readonly IBoxService boxService;
        private readonly ISetService setService;
        private readonly IProductService productService;
        private readonly IGlassCategoryService categoryService;
        private readonly ICommentService commentService;

        public ProductController(IGlassService glassService,
                                 IDecanterService decanterService,
                                 IBoxService boxService,
                                 ISetService setService,
                                 IProductService productService,
                                 IGlassCategoryService categoryService,
                                 ICommentService commentService)
        {
            this.glassService = glassService;
            this.decanterService = decanterService;
            this.boxService = boxService;
            this.setService = setService;
            this.productService = productService;
            this.categoryService = categoryService;
            this.commentService = commentService;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string id)
        {
            bool isGlass = await this.glassService.ExistsByIdAsync(id);
            if(isGlass)
            {
                return this.RedirectToAction("Details", "Glass", new { id });
            }

            bool isDecanter = await this.decanterService.ExistsByIdAsync(id);
            if(isDecanter)
            {
                return this.RedirectToAction("Details","Decanter",new { id });
            }

            bool isBox =  await this.boxService.ExistsByIdAsync(id);
            if(isBox)
            {
                return this.RedirectToAction("Details", "Box", new { id });
            }

            bool isSet = await this.setService.ExistsByIdAsync(id);
            if(isSet)
            {
                return this.RedirectToAction("Details", "Set", new { id });
            }

            this.TempData[ErrorMessage] = "This product does not exist! These are all the products you can choose.";
            return this.RedirectToAction("All", "Product");

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
        public async Task<IActionResult> Comment(string id)
        {
            bool isGlass = await this.glassService.ExistsByIdAsync(id);
            if (isGlass)
            {
                return this.RedirectToAction("Comment", "Glass", new { id });
            }

            bool isDecanter = await this.decanterService.ExistsByIdAsync(id);
            if (isDecanter)
            {
                return this.RedirectToAction("Comment", "Decanter", new { id });
            }

            bool isBox = await this.boxService.ExistsByIdAsync(id);
            if (isBox)
            {
                return this.RedirectToAction("Comment", "Box", new { id });
            }

            bool isSet = await this.setService.ExistsByIdAsync(id);
            if (isSet)
            {
                return this.RedirectToAction("Comment", "Set", new { id });
            }

            this.TempData[ErrorMessage] = "This product does not exist! These are all the products you can comment.";
            return this.RedirectToAction("All", "Product");
        }

        [HttpGet]
        public async Task<IActionResult> WriteComment(string id)
        {
            bool isGlass = await this.glassService.ExistsByIdAsync(id);
            if (isGlass)
            {
                return this.RedirectToAction("WriteComment", "Glass", new { id });
            }

            bool isDecanter = await this.decanterService.ExistsByIdAsync(id);
            if (isDecanter)
            {
                return this.RedirectToAction("WriteComment", "Decanter", new { id });
            }

            bool isBox = await this.boxService.ExistsByIdAsync(id);
            if (isBox)
            {
                return this.RedirectToAction("WriteComment", "Box", new { id });
            }

            bool isSet = await this.setService.ExistsByIdAsync(id);
            if (isSet)
            {
                return this.RedirectToAction("WriteComment", "Set", new { id });
            }

            this.TempData[ErrorMessage] = "This product does not exist! These are all the products you can comment.";
            return this.RedirectToAction("All", "Product");
        }

        [HttpGet]
        public async Task<IActionResult> WriteToComment(string id,string commentId)
        {
            bool isCommentidExist = await this.commentService.ExistsByIdAsync(commentId);

            if (string.IsNullOrEmpty(commentId) == false && isCommentidExist == false)
            {
                this.TempData[ErrorMessage] = "Comment with the provided id does not exist!";
                return this.RedirectToAction("Comment", "Pcoduct",new {id});
            }

            CommentFormModel model = new CommentFormModel();

            return this.View(model);
        }
        [HttpPost]
        public async Task<IActionResult> WriteToComment(string id,string commentId, CommentFormModel formModel)
        {            
            if (this.ModelState.IsValid == false)
            {
                return this.View(formModel);
            }

            try
            {
                string userId = User.GetId();
               await this.commentService.CreateCommentToCommentByUserIdAndByCommentIdAsync(userId!, formModel, commentId);
                TempData[SuccessMessage] = "Comment was added successfully!";
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, UnexpectedError);
                this.TempData[ErrorMessage] = UnexpectedError;
                return this.View(id);
            }

            return this.RedirectToAction("Comment", "Product", new { id });
        }
    }
}

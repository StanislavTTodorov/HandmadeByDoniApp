using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.Attributes;
using HandmadeByDoniApp.Web.Infrastructure.Extensions;
using HandmadeByDoniApp.Web.ViewModels.Comment;
using HandmadeByDoniApp.Web.ViewModels.Decanter;
using HandmadeByDoniApp.Web.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static HandmadeByDoniApp.Common.GeneralApplicationConstants;
using static HandmadeByDoniApp.Common.NotificationMessagesConstants;

namespace HandmadeByDoniApp.Web.Controllers
{
    public class DecanterController : BaseController
    {
        private readonly IDecanterService decanterService;

        public DecanterController(IDecanterService decanterService)
        {
            this.decanterService = decanterService;
        }

        [HttpGet]
        [IsAdmin]
        public IActionResult Add()
        {
            DecanterFormModel model = new DecanterFormModel();

            return this.View(model);
        }
        [HttpPost]
        [IsAdmin]
        public async Task<IActionResult> Add(DecanterFormModel formModel)
        {

            if (this.ModelState.IsValid == false)
            {
                return this.View(formModel);
            }

            try
            {
                await this.decanterService.CreateDecanterAsync(formModel);
                TempData[SuccessMessage] = "Decanter was added successfully!";
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, UnexpectedError);
                this.TempData[ErrorMessage] = UnexpectedError;
                return View(formModel);
            }

            return this.RedirectToAction("Index", "Home");

        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string id)
        {
            bool isDecanter = await this.decanterService.ExistsByIdAsync(id);
            if (isDecanter == false)
            {
                this.TempData[ErrorMessage] = "Decanter with the provided id does not exist!";
                return this.RedirectToAction("All", "Pcoduct");
            }

            try
            {
                DecanterDetailsViewModel viewModel = await this.decanterService.GetDecanterDetailsByIdAsync(id);
                return this.View(viewModel);
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = "Unexpected error occurred! Please try agenin later or contact administrator.";
                return this.RedirectToAction("All", "Product"); ;
            }
        }
        [HttpGet]
        public async Task<IActionResult> Comment(string id)
        {
            bool isDecanter = await this.decanterService.ExistsByIdAsync(id);
            if (isDecanter == false)
            {
                this.TempData[ErrorMessage] = "Decanter with the provided id does not exist!";
                return this.RedirectToAction("All", "Pcoduct");
            }

            try
            {

                AllProductCommentViewModel viewModel = await this.decanterService.GetDecanterCommentByIdAsync(id);
                return this.View("~/Views/Comment/Comment.cshtml", viewModel);

            }
            catch (Exception)
            {

                this.TempData[ErrorMessage] = "Unexpected error occurred! Please try agenin later or contact administrator.";
                return this.RedirectToAction("All", "Product");
            }

        }
        [HttpGet]
        public async Task<IActionResult> WriteComment(string id)
        {
            bool isDecanter = await this.decanterService.ExistsByIdAsync(id);
            if (isDecanter == false)
            {
                this.TempData[ErrorMessage] = "Decanter with the provided id does not exist!";
                return this.RedirectToAction("All", "Pcoduct");
            }

            CommentFormModel model = new CommentFormModel();

            return this.View("~/Views/Comment/WriteToComment.cshtml", model);
        }
        [HttpPost]
        public async Task<IActionResult> WriteComment(string id, CommentFormModel formModel)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.View("~/Views/Comment/WriteToComment.cshtml", formModel);
            }

            try
            {
                string userId = User.GetId();
                await this.decanterService.CreateCommentByUserIdAndByProductIdAsync(userId!, formModel, id);
                TempData[SuccessMessage] = "Comment was added successfully!";
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, UnexpectedError);
                this.TempData[ErrorMessage] = UnexpectedError;
            }

            return this.RedirectToAction("Comment", "Decanter", new { id });
        }
    }
}


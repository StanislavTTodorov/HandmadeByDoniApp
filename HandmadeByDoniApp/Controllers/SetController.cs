using Ganss.Xss;
using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.Infrastructure.Extensions;
using HandmadeByDoniApp.Web.ViewModels.Box;
using HandmadeByDoniApp.Web.ViewModels.Comment;
using HandmadeByDoniApp.Web.ViewModels.Product;
using HandmadeByDoniApp.Web.ViewModels.Set;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static HandmadeByDoniApp.Common.GeneralApplicationConstants;
using static HandmadeByDoniApp.Common.NotificationMessagesConstants;

namespace HandmadeByDoniApp.Web.Controllers
{
    public class SetController :BaseController
    {
        private readonly ISetService setService;
        private readonly ICommentService commentService;



        public SetController(ISetService setService,
                             ICommentService commentService)
        {
            this.setService = setService;
            this.commentService = commentService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string id)
        {
            bool isExists = await this.setService.ExistsByIdAsync(id);
            if (isExists == false)
            {
                this.TempData[ErrorMessage] = "Set with the provided id does not exist!";
                return this.RedirectToAction("All", "Pcoduct");
            }

            try
            {
                SetDetailsViewModel viewModel = await this.setService.GetSetDetailsByIdAsync(id);
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
            bool isExists = await this.setService.ExistsByIdAsync(id);
            if (isExists == false)
            {
                this.TempData[ErrorMessage] = "Set with the provided id does not exist!";
                return this.RedirectToAction("All", "Pcoduct");
            }

            try
            {

                AllProductCommentViewModel viewModel = await this.setService.GetSetCommentByIdAsync(id);
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
            bool isExists = await this.setService.ExistsByIdAsync(id);
            if (isExists == false)
            {
                this.TempData[ErrorMessage] = "Set with the provided id does not exist!";
                return this.RedirectToAction("All", "Pcoduct");
            }

            CommentFormModel model = new CommentFormModel();

            return this.View("~/Views/Comment/WriteToComment.cshtml", model);
        }
        [HttpPost]
        public async Task<IActionResult> WriteComment(string id, CommentFormModel formModel)
        {
            bool isExists = await this.setService.ExistsByIdAsync(id);
            if (isExists == false)
            {
                this.TempData[ErrorMessage] = "Set with the provided id does not exist!";
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
                await this.setService.CreateCommentByUserIdAndByProductIdAsync(userId!, formModel, id);
                TempData[SuccessMessage] = "Comment was added successfully!";
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, UnexpectedError);
                this.TempData[ErrorMessage] = UnexpectedError;
            }

            return this.RedirectToAction("Comment", "Set", new { id });
        }
    }
}


using HandmadeByDoniApp.Data.Models;
using HandmadeByDoniApp.Services.Data.Interfaces;

using HandmadeByDoniApp.Web.Infrastructure.Extensions;
using HandmadeByDoniApp.Web.ViewModels.Box;
using HandmadeByDoniApp.Web.ViewModels.Comment;

using HandmadeByDoniApp.Web.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static HandmadeByDoniApp.Common.GeneralApplicationConstants;
using static HandmadeByDoniApp.Common.NotificationMessagesConstants;

namespace HandmadeByDoniApp.Web.Controllers
{
    public class BoxController : BaseController
    {
        private readonly IBoxService boxService;
        private readonly ICommentService commentService;
      


        public BoxController(IBoxService boxService,
                             ICommentService commentService)
        {
             this.boxService = boxService;
             this.commentService = commentService;
        }      

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string id)
        {
            bool isBox = await this.boxService.ExistsByIdAsync(id);
            if (isBox == false)
            {
                this.TempData[ErrorMessage] = "Box with the provided id does not exist!";
                return this.RedirectToAction("All", "Pcoduct");
            }

            try
            {
               BoxDetailsViewModel viewModel = await this.boxService.GetBoxDetailsByIdAsync(id);
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
            bool isBoxExists = await this.boxService.ExistsByIdAsync(id);
            if (isBoxExists == false)
            {
                this.TempData[ErrorMessage] = "Box with the provided id does not exist!";
                return this.RedirectToAction("All", "Pcoduct");
            }         

            try
            {

                AllProductCommentViewModel viewModel = await this.boxService.GetBoxCommentByIdAsync(id);
                return this.View("~/Views/Comment/Comment.cshtml", viewModel);
               // return this.View(viewModel);

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
            bool isBox = await this.boxService.ExistsByIdAsync(id);
            if (isBox == false)
            {
                this.TempData[ErrorMessage] = "Box with the provided id does not exist!";
                return this.RedirectToAction("All", "Pcoduct");
            }

            CommentFormModel model = new CommentFormModel();

            return this.View("~/Views/Comment/WriteToComment.cshtml", model);
        }
        [HttpPost]
        public async Task<IActionResult> WriteComment(string id, CommentFormModel formModel)
        {
            bool isBox = await this.boxService.ExistsByIdAsync(id);
            if (isBox == false)
            {
                this.TempData[ErrorMessage] = "Box with the provided id does not exist!";
                return this.RedirectToAction("All", "Pcoduct");
            }

            if (this.ModelState.IsValid == false)
            {
                return this.View("~/Views/Comment/WriteToComment.cshtml", formModel);
            }

            try
            {
                string userId = User.GetId();
                await this.boxService.CreateCommentByUserIdAndByProductIdAsync(userId!, formModel, id);
                TempData[SuccessMessage] = "Comment was added successfully!";
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, UnexpectedError);
                this.TempData[ErrorMessage] = UnexpectedError;
            }

            return this.RedirectToAction("Comment", "Box", new { id });
        }
    }
}


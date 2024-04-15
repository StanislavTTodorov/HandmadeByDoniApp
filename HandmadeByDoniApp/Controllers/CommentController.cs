using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Services.Data.Service;
using HandmadeByDoniApp.Web.Infrastructure.Extensions;
using HandmadeByDoniApp.Web.ViewModels.Comment;
using Microsoft.AspNetCore.Mvc;
using static HandmadeByDoniApp.Common.NotificationMessagesConstants;
using static HandmadeByDoniApp.Common.GeneralApplicationConstants;
using Ganss.Xss;


namespace HandmadeByDoniApp.Web.Controllers
{
    public class CommentController : BaseController
    {
        private readonly ICommentService commentService;
        private readonly IGlassService glassService;
        private readonly IDecanterService decanterService;
        private readonly ISetService setService;
        private readonly IBoxService boxService;

        public CommentController(ICommentService commentService,
                                 IBoxService boxService,
                                 IGlassService glassService,
                                 IDecanterService decanterService,
                                 ISetService setService)
        {
            this.commentService = commentService;
            this.glassService = glassService;
            this.decanterService = decanterService;
            this.boxService = boxService;
            this.setService = setService;
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id, string commentId)
        {
            bool commentExists = await this.commentService.ExistsByIdAsync(commentId);
            if (!commentExists)
            {
                TempData[ErrorMessage] = "Comment with the provided id does not exist!";

                return RedirectToAction("All", "Product");

                //return this.NotFound(); -> to return 404 page
            }
            bool isYourComment = await this.commentService.HasUserCommentByUserIdAndByCommentIdAsync(User.GetId(), commentId);
            if (!isYourComment)
            {
                TempData[ErrorMessage] = "You must be the user, who wrote the comment, which you want to edit!";

                return RedirectToAction("Comment", "Product",new {id});
            }
            try
            {
                CommentFormModel formModel = await commentService.GetCommentForEditByIdAsync(commentId);

                return View(formModel);
            }
            catch (Exception)
            {
                return GeneralError();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, string commentId,CommentFormModel formModel)
        {
            var sanitizer = new HtmlSanitizer();
            formModel.Text = sanitizer.Sanitize(formModel.Text);

            if (this.ModelState.IsValid == false)
            {
                return this.View(formModel);
            }

            bool commentExists = await this.commentService.ExistsByIdAsync(commentId);
            if (!commentExists)
            {
                TempData[ErrorMessage] = "Comment with the provided id does not exist!";

                return RedirectToAction("All", "Product");

                //return this.NotFound(); -> to return 404 page
            }

            bool isYourComment = await this.commentService.HasUserCommentByUserIdAndByCommentIdAsync(User.GetId(), commentId);
            if (!isYourComment && !User.IsAdmin())
            {
                TempData[ErrorMessage] = "You must be the user, who wrote the comment, which you want to edit!";

                return RedirectToAction("Comment", "Product", new { id });
            }

            try
            {
                await this.commentService.EditCommentByIdAndFormModelAsync(commentId, formModel);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty,
                    "Unexpected error occurred while trying to edit the comment. Please try again later or contact administrator!");

                return View(formModel);
            }

            TempData[SuccessMessage] = "Comment was edited successfully!";
            return RedirectToAction("Comment", "Comment", new { id });
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
        public async Task<IActionResult> WriteToComment(string id, string commentId)
        {
            bool isCommentidExist = await this.commentService.ExistsByIdAsync(commentId);

            if (isCommentidExist == false)
            {
                this.TempData[ErrorMessage] = "Comment with the provided id does not exist!";
                return this.RedirectToAction("Comment", "Pcoduct", new { id });
            }

            CommentFormModel model = new CommentFormModel(); 

            return this.View(model);
        }
        [HttpPost]
        public async Task<IActionResult> WriteToComment(string id, string commentId, CommentFormModel formModel)
        {
            var sanitizer = new HtmlSanitizer();
            formModel.Text = sanitizer.Sanitize(formModel.Text);

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

            return this.RedirectToAction("Comment", "Comment", new { id });
        }
        private IActionResult GeneralError()
        {
            TempData[ErrorMessage] =
                "Unexpected error occurred! Please try again later or contact administrator";

            return RedirectToAction("Index", "Home");
        }
    }
}

using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.Infrastructure.Extensions;
using HandmadeByDoniApp.Web.ViewModels.Comment;
using Microsoft.AspNetCore.Mvc;
using static HandmadeByDoniApp.Common.NotificationMessagesConstants;
using static HandmadeByDoniApp.Common.GeneralMessages;
using Ganss.Xss;
using HandmadeByDoniApp.Data.Models;
//using HandmadeByDoniApp.Web.Resources;
using Microsoft.Extensions.Localization;
using Resources.Resources;


namespace HandmadeByDoniApp.Web.Controllers
{
    public class CommentController : BaseController<CommentController>
    {
        private readonly ICommentService commentService;
        private IStringLocalizer<App> L;

        public CommentController(ICommentService commentService, IStringLocalizer<App> L)
        {
            this.commentService = commentService;  
            this.L = L;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id, string commentId)
        {
            bool commentExists = await this.commentService.ExistsByIdAsync(commentId);
            if (!commentExists)
            {
                this.TempData[ErrorMessage] = $"{L[nameof(Comment)].Value} {L["ProductNotExist"].Value}"; //string.Format(ProductNotExist, nameof(Comment));

                return this.RedirectToAction("All", "Product");

                //return this.NotFound(); -> to return 404 page
            }
            bool isYourComment = await this.commentService.HasUserCommentByUserIdAndByCommentIdAsync(User.GetId(), commentId);
            if (!isYourComment)
            {
                this.TempData[ErrorMessage] = L["EditComment"].Value;//EditComment;

                return this.RedirectToAction("Comment", "Product",new {id});
            }
            try
            {
                CommentFormModel formModel = await commentService.GetCommentForEditByIdAsync(commentId);

                return this.View(formModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
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
                this.TempData[ErrorMessage] = $"{L[nameof(Comment)]} {L["ProductNotExist"].Value}";  //string.Format(ProductNotExist,nameof(Comment));

                return this.RedirectToAction("All", "Product");

                //return this.NotFound(); -> to return 404 page
            }

            bool isYourComment = await this.commentService.HasUserCommentByUserIdAndByCommentIdAsync(User.GetId(), commentId);
            if (!isYourComment && !User.IsAdmin())
            {
                this.TempData[ErrorMessage] = L["EditComment"].Value;// EditComment;

                return this.RedirectToAction("Comment", "Product", new { id });
            }

            try
            {
                await this.commentService.EditCommentByIdAndFormModelAsync(commentId, formModel);
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, string.Format(UnexpectedErrorTryingTo, $"edit the {nameof(Comment)}"));
                this.TempData[ErrorMessage] = $"{L["UnexpectedErrorTryingTo"].Value} {L["addNew"].Value} {L[$"{nameof(Comment)}"].Value}";

                return this.View(formModel);
            }

            this.TempData[SuccessMessage] = $"{L[nameof(Comment)].Value} {L["EditSuccessfully"].Value}"; // string.Format(EditSuccessfully,nameof(Comment));
            return this.RedirectToAction("Comment", "Product", new { id });
        }        

        [HttpGet]
        public async Task<IActionResult> WriteToComment(string id, string commentId)
        {
            bool isCommentidExist = await this.commentService.ExistsByIdAsync(commentId);

            if (isCommentidExist == false)
            {
                this.TempData[ErrorMessage] = $"{L[nameof(Comment)].Value} {L["ProductNotExist"].Value}"; //string.Format(ProductNotExist,nameof(Comment));
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
                this.TempData[SuccessMessage] = $"{L[nameof(Comment)].Value}   {L["AddSuccessfully"].Value}"; //string.Format(AddSuccessfully,nameof(Comment));
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, string.Format(UnexpectedErrorTryingTo, $"add new {nameof(Comment)}"));
                this.TempData[ErrorMessage] = $"{L["UnexpectedErrorTryingTo"].Value} {L["addNew"].Value} {L[$"{nameof(Comment)}"].Value}";//string.Format(UnexpectedErrorTryingTo, $"add new {nameof(Comment)}");
                return this.View(id);
            }

            return this.RedirectToAction("Comment", "Product", new { id });
        }

        private IActionResult GeneralError()
        {
            this.TempData[ErrorMessage] = L["UnexpectedError"].Value; // UnexpectedError;

            return this.RedirectToAction("Index", "Home");
        }
    }
}

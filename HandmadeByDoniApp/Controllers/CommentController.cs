using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.Infrastructure.Extensions;
using HandmadeByDoniApp.Web.ViewModels.Comment;
using Microsoft.AspNetCore.Mvc;
using static HandmadeByDoniApp.Common.NotificationMessagesConstants;
using static HandmadeByDoniApp.Common.GeneralMessages;
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
                this.TempData[ErrorMessage] = string.Format(ProductNotExist, nameof(Comment));

                return this.RedirectToAction("All", "Product");

                //return this.NotFound(); -> to return 404 page
            }
            bool isYourComment = await this.commentService.HasUserCommentByUserIdAndByCommentIdAsync(User.GetId(), commentId);
            if (!isYourComment)
            {
                this.TempData[ErrorMessage] = EditComment;

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
                this.TempData[ErrorMessage] = string.Format(ProductNotExist,nameof(Comment));

                return this.RedirectToAction("All", "Product");

                //return this.NotFound(); -> to return 404 page
            }

            bool isYourComment = await this.commentService.HasUserCommentByUserIdAndByCommentIdAsync(User.GetId(), commentId);
            if (!isYourComment && !User.IsAdmin())
            {
                this.TempData[ErrorMessage] = EditComment;

                return this.RedirectToAction("Comment", "Product", new { id });
            }

            try
            {
                await this.commentService.EditCommentByIdAndFormModelAsync(commentId, formModel);
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, string.Format(UnexpectedErrorTryingTo, $"edit the {nameof(Comment)}"));
                return this.View(formModel);
            }

            this.TempData[SuccessMessage] = string.Format(EditSuccessfully,nameof(Comment));
            return this.RedirectToAction("Comment", "Comment", new { id });
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

            this.TempData[ErrorMessage] = CommentNotExist;
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

            this.TempData[ErrorMessage] = CommentNotExist;
            return this.RedirectToAction("All", "Product");
        }

        [HttpGet]
        public async Task<IActionResult> WriteToComment(string id, string commentId)
        {
            bool isCommentidExist = await this.commentService.ExistsByIdAsync(commentId);

            if (isCommentidExist == false)
            {
                this.TempData[ErrorMessage] = string.Format(ProductNotExist,nameof(Comment));
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
                this.TempData[SuccessMessage] = string.Format(AddSuccessfully,nameof(Comment));
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, string.Format(UnexpectedErrorTryingTo, $"add new {nameof(Comment)}"));
                this.TempData[ErrorMessage] = string.Format(UnexpectedErrorTryingTo, $"add new {nameof(Comment)}");
                return this.View(id);
            }

            return this.RedirectToAction("Comment", "Comment", new { id });
        }

        private IActionResult GeneralError()
        {
            this.TempData[ErrorMessage] = UnexpectedError;

            return this.RedirectToAction("Index", "Home");
        }
    }
}

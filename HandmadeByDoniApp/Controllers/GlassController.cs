using Ganss.Xss;
using HandmadeByDoniApp.Data.Models;
using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.Attributes;
using HandmadeByDoniApp.Web.Infrastructure.Extensions;
using HandmadeByDoniApp.Web.ViewModels.Comment;
using HandmadeByDoniApp.Web.ViewModels.Glass;
using HandmadeByDoniApp.Web.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using static HandmadeByDoniApp.Common.GeneralApplicationConstants;
using static HandmadeByDoniApp.Common.NotificationMessagesConstants;

namespace HandmadeByDoniApp.Web.Controllers
{
    public class GlassController : BaseController
    {
        private readonly IGlassService glassService;
        private readonly IGlassCategoryService glassCategoryServise;
        public GlassController(IGlassCategoryService glassCategoryServise,
                                IGlassService glassService)
        {
            this.glassService = glassService;
            this.glassCategoryServise = glassCategoryServise;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string id)
        {
            bool isGlass = await this.glassService.ExistsByIdAsync(id);
            if (isGlass == false)
            {
                this.TempData[ErrorMessage] = "Glass with the provided id does not exist!";
                return this.RedirectToAction("All", "Pcoduct");
            }

            try
            {
                GlassDetailsViewModel viewModel = await this.glassService.GetGlassDetailsByIdAsync(id);
                return this.View(viewModel);
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = "Unexpected error occurred! Please try agenin later or contact administrator.";
                return this.RedirectToAction("All", "Product");
            }

        }
        [HttpGet]
        public async Task<IActionResult> Comment(string id)
        {
            bool isGlass = await this.glassService.ExistsByIdAsync(id);
            if (isGlass == false)
            {
                this.TempData[ErrorMessage] = "Glass with the provided id does not exist!";
                return this.RedirectToAction("All", "Pcoduct");
            }

            try
            {

                AllProductCommentViewModel viewModel = await this.glassService.GetGlassCommentByIdAsync(id);
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
            bool isGlass = await this.glassService.ExistsByIdAsync(id);
            if (isGlass == false)
            {
                this.TempData[ErrorMessage] = "Glass with the provided id does not exist!";
                return this.RedirectToAction("All", "Pcoduct");
            }

            CommentFormModel model = new CommentFormModel();

            return this.View("~/Views/Comment/WriteToComment.cshtml", model);
        }
        [HttpPost]
        public async Task<IActionResult> WriteComment(string id, CommentFormModel formModel)
        {
            bool isGlass = await this.glassService.ExistsByIdAsync(id);
            if (isGlass == false)
            {
                this.TempData[ErrorMessage] = "Glass with the provided id does not exist!";
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
                await this.glassService.CreateCommentByUserIdAndByProductIdAsync(userId!, formModel, id);
                TempData[SuccessMessage] = "Comment was added successfully!";
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, UnexpectedError);
                this.TempData[ErrorMessage] = UnexpectedError;
            }

            return this.RedirectToAction("Comment", "Glass", new { id });
        }
    }
}

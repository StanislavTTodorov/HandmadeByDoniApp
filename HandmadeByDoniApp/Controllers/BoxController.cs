using HandmadeByDoniApp.Data.Models;
using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.ViewModels.Box;
using HandmadeByDoniApp.Web.ViewModels.Glass;
using Microsoft.AspNetCore.Mvc;
using static HandmadeByDoniApp.Common.GeneralApplicationConstants;
using static HandmadeByDoniApp.Common.NotificationMessagesConstants;

namespace HandmadeByDoniApp.Web.Controllers
{
    public class BoxController : Controller
    {
        private readonly IBoxService boxService;
        public BoxController(IBoxService boxService)
        {
             this.boxService = boxService;
        }
        [HttpGet]
        public IActionResult Add()
        {
            // TODO Authorize only for the Admin
            BoxFormModel model = new BoxFormModel();
         
            return this.View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(BoxFormModel formModel)
        {
            // TODO Authorize only for the Admin

            if (this.ModelState.IsValid == false)
            {
                return this.View(formModel);
            }

            try
            {
                await this.boxService.CreateBoxAsync(formModel);
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty,UnexpectedError);
                this.TempData[ErrorMessage] = UnexpectedError;
                return View(formModel);
            }

            return this.RedirectToAction("Index", "Home");

        }
        [HttpGet]
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
    }
}

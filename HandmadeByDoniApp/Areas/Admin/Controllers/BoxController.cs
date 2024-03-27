using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.Attributes;
using HandmadeByDoniApp.Web.ViewModels.Box;
using Microsoft.AspNetCore.Mvc;
using static HandmadeByDoniApp.Common.NotificationMessagesConstants;
using static HandmadeByDoniApp.Common.GeneralApplicationConstants;



namespace HandmadeByDoniApp.Web.Areas.Admin.Controllers
{
    public class BoxController : BaseAdminController
    {
        private readonly IBoxService boxService;



        public BoxController(IBoxService boxService)
        {
            this.boxService = boxService;
        }
        [HttpGet]
        public IActionResult Add()
        {
            BoxFormModel model = new BoxFormModel();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(BoxFormModel formModel)
        {

            if (this.ModelState.IsValid == false)
            {
                return this.View(formModel);
            }

            try
            {
                await this.boxService.CreateBoxAsync(formModel);
                TempData[SuccessMessage] = "Box was added successfully!";
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, UnexpectedError);
                this.TempData[ErrorMessage] = UnexpectedError;
                return View(formModel);
            }

            return this.RedirectToAction("Index", "Home", new { area = "" });

        }

    }
}

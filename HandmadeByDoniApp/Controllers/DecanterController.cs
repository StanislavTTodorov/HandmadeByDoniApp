using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.ViewModels.Decanter;
using Microsoft.AspNetCore.Mvc;
using static HandmadeByDoniApp.Common.GeneralApplicationConstants;
using static HandmadeByDoniApp.Common.NotificationMessagesConstants;

namespace HandmadeByDoniApp.Web.Controllers
{
    public class DecanterController : Controller
    {
        private readonly IDecanterService decanterService;

        public DecanterController(IDecanterService decanterService)
        {
            this.decanterService = decanterService;
        }
        [HttpGet]
        public IActionResult Add()
        {
            // TODO Authorize only for the Admin
            DecanterFormModel model = new DecanterFormModel();

            return this.View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(DecanterFormModel formModel)
        {
            // TODO Authorize only for the Admin

            if (this.ModelState.IsValid == false)
            {
                return this.View(formModel);
            }

            try
            {
                await this.decanterService.CreateDecanterAsync(formModel);
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, UnexpectedError);
                this.TempData[ErrorMessage] = UnexpectedError;
                return View(formModel);
            }

            return this.RedirectToAction("Index", "Home");

        }
    }
}

using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.ViewModels.Set;
using Microsoft.AspNetCore.Mvc;

namespace HandmadeByDoniApp.Web.Areas.Admin.Controllers
{
    public class SetController : BaseAdminController
    {
        private readonly ISetService setService;

        public SetController(ISetService setService)
        {
            this.setService = setService;
        }


        [HttpGet]
        public async Task<IActionResult> Add()
        { 

           SetAddFormModel model = new SetAddFormModel();
            model.Glasses = await this.setService.GetGlassesInSetAsync();
            model.Decanters = await this.setService.GetDecantersInSetAsync();
            return View(model);
        }
    }
}

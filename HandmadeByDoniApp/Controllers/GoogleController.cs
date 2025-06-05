
//using HandmadeByDoniApp.Web.Resources;

using static HandmadeByDoniApp.Common.NotificationMessagesConstants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;
using Resources.Resources;

namespace HandmadeByDoniApp.Web.Controllers
{
    public class GoogleController : BaseController<GoogleController>
    {
        private IStringLocalizer<App> L;
        public GoogleController(IStringLocalizer<App> L)
        {
            this.L = L;           
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Google()
        {         
            this.TempData[ErrorMessage] = $"{L["В процес на разработка"].Value}";
            return this.Redirect("/User/Login");
        }
    }
}


using HandmadeByDoniApp.Web.Resources;

using static HandmadeByDoniApp.Common.NotificationMessagesConstants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace HandmadeByDoniApp.Web.Controllers
{
    public class GoogleController : BaseController<GoogleController>
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Google()
        {         
            this.TempData[ErrorMessage] = $"{L["В процес на разработка"]}";
            return this.Redirect("/User/Login");
        }
    }
}

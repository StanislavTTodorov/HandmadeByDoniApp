using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static HandmadeByDoniApp.Common.GeneralApplicationConstants;

namespace HandmadeByDoniApp.Web.Areas.Admin.Controllers
{
    [Area(AdminAreaName)]
    [Authorize(Roles =AdminiRoleName)]
    public class BaseAdminController<T> : Controller where T : BaseAdminController<T>
    {
        private ILogger<T>? logger;

        protected ILogger<T>? Logger
            => this.logger ??= HttpContext.RequestServices.GetRequiredService<ILogger<T>>();
    }
}

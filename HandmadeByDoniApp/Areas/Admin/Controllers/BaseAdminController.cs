using HandmadeByDoniApp.Web.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using static HandmadeByDoniApp.Common.GeneralApplicationConstants;

namespace HandmadeByDoniApp.Web.Areas.Admin.Controllers
{
    [Area(AdminAreaName)]
    [Authorize(Roles =AdminiRoleName)]
    public class BaseAdminController<T> : Controller where T : BaseAdminController<T>
    {
        private ILogger<T>? logger;
        public IStringLocalizer<App> L;

        protected ILogger<T>? Logger
            => this.logger ??= HttpContext.RequestServices.GetRequiredService<ILogger<T>>();

        public IStringLocalizer<App> l
   => this.L ??= HttpContext.RequestServices.GetRequiredService<IStringLocalizer<App>>();

    }
}

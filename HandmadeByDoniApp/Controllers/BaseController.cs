using HandmadeByDoniApp.Web.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace HandmadeByDoniApp.Web.Controllers
{
    [Authorize]
    public class BaseController<T> : Controller where T : BaseController<T>
    {
        private ILogger<T>? logger;
        public IStringLocalizer<App> L;

        protected ILogger<T>? Logger
            => this.logger ??= HttpContext.RequestServices.GetRequiredService<ILogger<T>>();

        public IStringLocalizer<App> l
           => this.L ??= HttpContext.RequestServices.GetRequiredService<IStringLocalizer<App>>();
    }
}

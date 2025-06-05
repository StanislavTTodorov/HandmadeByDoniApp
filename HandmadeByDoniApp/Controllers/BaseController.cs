
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace HandmadeByDoniApp.Web.Controllers
{
    [Authorize]
    public class BaseController<T> : Controller where T : BaseController<T>
    {
        private ILogger<T>? logger;
        //private IStringLocalizer<App>? localizer;

        protected ILogger<T> Logger
            => this.logger ??= HttpContext.RequestServices.GetRequiredService<ILogger<T>>();

        //protected IStringLocalizer<App> L
        //    => this.localizer ??= HttpContext.RequestServices.GetRequiredService<IStringLocalizer<App>>();
    }
}


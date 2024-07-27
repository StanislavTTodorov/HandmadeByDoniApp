using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HandmadeByDoniApp.Web.Controllers
{
    [Authorize]
    public class BaseController<T> : Controller where T : BaseController<T>
    {
        private ILogger<T>? logger;

        protected ILogger<T>? Logger
            => this.logger ??= HttpContext.RequestServices.GetRequiredService<ILogger<T>>();
    }
}

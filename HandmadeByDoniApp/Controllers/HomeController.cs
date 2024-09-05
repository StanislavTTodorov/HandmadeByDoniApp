using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.ViewModels.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Text.RegularExpressions;

namespace HandmadeByDoniApp.Web.Controllers
{
    public class HomeController : BaseController<HomeController>
    {
        private readonly IProductService product;
        private readonly IStringLocalizer localizer;

        public HomeController(IProductService product, IStringLocalizer localizer)
        {
            this.product = product;
            this.localizer = localizer;
        }
        [AllowAnonymous]
        public IActionResult SetCulture(string culture, string returnUrl)
        {
            if (!string.IsNullOrEmpty(culture))
            {
                var cultureInfo = new CultureInfo(culture);
                CultureInfo.CurrentCulture = cultureInfo;
                CultureInfo.CurrentUICulture = cultureInfo;
                // Задаване на културата в cookie
                Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                    new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );
            }

            return LocalRedirect(returnUrl); // Пренасочете обратно към страницата, откъдето идва заявката
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            IEnumerable<IndexViewModel> viewModels =
                await this.product.LastTwelveProductsAsync();
            return this.View(viewModels);
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {
            if (statusCode == 400 || statusCode == 404)
            {
                return this.View("Error404");
            }

            if (statusCode == 401)
            {
                return this.View("Error401");
            }

            return this.View();
        }
    }
}
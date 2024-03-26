using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.ViewModels.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HandmadeByDoniApp.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IProductService product;

        public HomeController(IProductService product)
        {
            this.product = product;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            IEnumerable<IndexViewModel> viewModels =
                await this.product.LastTwelveProductsAsync();
            return View(viewModels);
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {
            if (statusCode == 400 || statusCode == 404)
            {
                return View("Error404");
            }

            if (statusCode == 401)
            {
                return View("Error401");
            }

            return View();
        }
    }
}
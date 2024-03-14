using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HandmadeByDoniApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IProductService product;

        public HomeController(ILogger<HomeController> logger,
                              IProductService product)
        {
            this.logger = logger;
            this.product = product;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<IndexViewModel> viewModels =
                await this.product.LastTwelveProductsAsync();
            return View(viewModels);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
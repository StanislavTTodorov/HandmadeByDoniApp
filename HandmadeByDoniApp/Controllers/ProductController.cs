using Microsoft.AspNetCore.Mvc;

namespace HandmadeByDoniApp.Web.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HandmadeByDoniApp.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        
    }
}

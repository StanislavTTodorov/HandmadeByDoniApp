using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static HandmadeByDoniApp.Common.GeneralApplicationConstants;

namespace HandmadeByDoniApp.Web.Areas.Admin.Controllers
{
    [Area(AdminAreaName)]
    [Authorize(Roles =AdminiRoleName)]
    public class BaseAdminController : Controller
    {
        
    }
}


using HandmadeByDoniApp.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using static HandmadeByDoniApp.Common.NotificationMessagesConstants;

namespace HandmadeByDoniApp.Web.Attributes
{
    public class IsAdminAttribute:ActionFilterAttribute
    {           
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);           
            if (context.HttpContext.User.IsAdmin() == false)
            {
                Controller? controller = (context.Controller as Controller);
                if (controller != null)
                {
                    controller.TempData[ErrorMessage] = "You do not have access! ";
                }
                                           
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    { "controller", "Home" },
                     { "action", "Index" }

                });
            }         
        }
    }
}


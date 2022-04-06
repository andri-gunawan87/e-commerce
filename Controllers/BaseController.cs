using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace e_commerce.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (HttpContext.User == null || HttpContext.User.Identity == null)
            {
                ViewBag.IsLogged = false;
            }
            else
            {
                ViewBag.IsLogged = HttpContext.User.Identity.IsAuthenticated;
                ViewBag.IsAdmin = HttpContext.User.IsInRole(AppConstant.ADMIN);
            }

            base.OnActionExecuted(context);
        }
    }
}

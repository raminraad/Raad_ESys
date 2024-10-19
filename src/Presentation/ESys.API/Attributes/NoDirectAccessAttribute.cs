using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ESys.API.Attributes;
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class NoDirectAccessAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        var referer = filterContext.HttpContext.Request.Headers["Referer"].ToString();
        var host = filterContext.HttpContext.Request.Host.Host;

        if (string.IsNullOrEmpty(referer) || new Uri(referer).Host != host)
        {
            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary(new { controller = "Home", action = "Index", area = "" }));
        }

    }
}
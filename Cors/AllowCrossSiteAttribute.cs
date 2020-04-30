using Microsoft.AspNetCore.Mvc.Filters;

namespace SaveASoul.Cors
{


    public class AllowCrossSiteAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {  
            filterContext.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:4200");
            filterContext.HttpContext.Response.Headers.Add("Access-Control-Allow-Headers", "*");
            filterContext.HttpContext.Response.Headers.Add("Access-Control-Allow-Credentials", "true");

            base.OnActionExecuting(filterContext);
        }
    }
}

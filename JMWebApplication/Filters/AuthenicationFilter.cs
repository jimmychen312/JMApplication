using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace JMWebApplication.Filters
{

    public class AuthenicationAction : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            HttpContext ctx = default(HttpContext);
            ctx = HttpContext.Current;
       
            base.OnActionExecuting(context);

            if (ctx.User.Identity.IsAuthenticated == false)
            {                               
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Customers", action = "CustomerLogin" }));               
            }
           
        }

    } 

}
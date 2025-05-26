using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace culinaryConnect.Web.Filters
{
	public class AuthorizeAdminAttribute: ActionFilterAttribute
	{
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = filterContext.HttpContext.Session;

            var path = filterContext.HttpContext.Request.Path.ToLower();
            if (path.Contains("/admin/login") || path.Contains("/admin/loginrequest"))
                return;

            if (session["AdminID"] == null)
            {
                filterContext.Result = new RedirectResult("~/Admin/Login");
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
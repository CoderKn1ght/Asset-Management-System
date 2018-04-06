using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Asset_Management_System.Data;

namespace Asset_Management_System.Authorize
{
    [AttributeUsage(AttributeTargets.All)]
    public class CustomLoginFilter : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return HttpContext.Current.Session["UserId"] != null;
        }
    }
}
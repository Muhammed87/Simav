using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Text;

namespace App.Common.Filters
{
    public class AutFilter : System.Web.Http.Filters.FilterAttribute, Microsoft.AspNetCore.Mvc.Filters.IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if(context.HttpContext.Session.GetString("OturumAcik")==null || 
                !Convert.ToBoolean(context.HttpContext.Session.GetString("OturumAcik")))
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new {controller="Kullanici",action="Login"}
                        ));
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SITQnAAPIServiceADFS
{
    public class WebApiApplication : System.Web.HttpApplication
    {
       
        //AreaRegistration.RegisterAllAreas();
        //GlobalConfiguration.Configure(WebApiConfig.Register);
        //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        //RouteConfig.RegisterRoutes(RouteTable.Routes);
        //BundleConfig.RegisterBundles(BundleTable.Bundles);

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        //protected void Application_BeginRequest(Object sender, EventArgs e)
        //{
        //    var req = HttpContext.Current.Request;
        //    var res = HttpContext.Current.Response;

        //    if (req.AcceptTypes != null && req.AcceptTypes.Contains("application/json"))
        //    {
        //        res.ContentType = "application/json; charset=UTF-8";
        //    }
        //    else
        //    {
        //        res.ContentType = "text/plain; charset=UTF-8";
        //    }

        //    if (req.HttpMethod == "OPTIONS")
        //    {
        //        res.Headers.Add("Access-Control-Allow-Headers", "Content-Type");
        //        res.StatusCode = 200;
        //        res.End();
        //    }
        //}

    }
}

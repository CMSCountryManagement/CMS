using CMS_API.Mappings;
using CMS_API.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CMS_API
{
	public class WebApiApplication : System.Web.HttpApplication
	{
        protected void Application_EndRequest(object sender, EventArgs args)
        {
            ApiEvent objevent = new ApiEvent();
            objevent.Logging();
        }  

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			GlobalConfiguration.Configure(WebApiConfig.Register);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

            CountryMapping.Configure();
		}
	}
}

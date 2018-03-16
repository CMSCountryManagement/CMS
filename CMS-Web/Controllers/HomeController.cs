using CMS_Entity.Db;
using CMS_Web.Client;
using CMS_Web.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS_Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
			CMSApiClient client = new CMSApiClient(HttpContext);

			var result = client.Get<User>(APIUrls.LoggedInUser);

			return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
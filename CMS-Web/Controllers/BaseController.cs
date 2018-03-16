using CMS_Web.Client;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CMS_Web.Controllers
{
	public class BaseController : Controller
	{
		protected CMSApiClient client;

		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			client = new CMSApiClient(HttpContext);
		}

		#region Helpers

		[NonAction]
		protected ActionResult BaseView<T>(IRestResponse<T> result)
		{
			if (result.StatusCode != HttpStatusCode.OK)
			{
				return new HttpStatusCodeResult(result.StatusCode);
			}

			return View(result.Data);
		}

		[NonAction]
		protected ActionResult BaseRedirectToAction(IRestResponse result, string action)
		{
			if (result.StatusCode != HttpStatusCode.OK)
			{
				return new HttpStatusCodeResult(result.StatusCode);
			}

			return RedirectToAction(action);
		}

		[NonAction]
		protected ActionResult BaseView(IRestResponse result, string action)
		{
			if (result.StatusCode != HttpStatusCode.OK)
			{
				return new HttpStatusCodeResult(result.StatusCode);
			}

			return View(action);
		}
		#endregion
	}
}
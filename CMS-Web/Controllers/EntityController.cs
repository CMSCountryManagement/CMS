using CMS_Web.Client;
using CMS_Web.Constants;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CMS_Web.Controllers
{
	public class EntityController<T> : BaseController where T : new()
	{
		private readonly string _url;

		public EntityController(string url)
		{
			_url = url;
		}

		// GET: Controller
		public ActionResult Index()
		{
			var result = client.Get<List<T>>(_url);
			return BaseView<List<T>>(result);
		}

		// GET: Controller/Details/{id}
		public ActionResult Details(Guid id)
		{
			var result = GetById(id);
			return BaseView<T>(result);
		}

		// GET: Controller/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Controller/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(T entity)
		{
			if (ModelState.IsValid)
			{
				var result = client.Post(_url, entity);
				return BaseRedirectToAction(result, "Index");
			}

			return View("Index");
		}

		// GET: Controller/Edit/{id}
		public ActionResult Edit(Guid id)
		{
			var result = GetById(id);
			return BaseView<T>(result);
		}

		// POST: Controller/Edit/{id}
		[HttpPost]
		public ActionResult Edit(Guid id, T item)
		{
			try
			{
				var result = client.Put(GetIdUrl(id), item);

				return BaseRedirectToAction(result, "Index");
			}
			catch
			{
				return View();
			}
		}

		// GET: Controller/Delete/{id}
		public ActionResult Delete(Guid id)
		{
			var result = GetById(id);
			return BaseView<T>(result);
		}

		// POST: Controller/Delete/{id}
		[HttpPost]
		public ActionResult Delete(Guid id, T collection)
		{
			try
			{
				var result = client.Delete(GetIdUrl(id));

				return BaseRedirectToAction(result, "Index");				
			}
			catch
			{
				return View();
			}
		}

		#region Helpers

		protected IRestResponse<T> GetById(Guid id)
		{
			return client.Get<T>(GetIdUrl(id));
		}

		protected string GetIdUrl(Guid id)
		{
			return String.Format("{0}/{1}", _url, id.ToString());
		}
		#endregion
	}
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMS_Entity.Db;
using CMS_Web.Constants;

namespace CMS_Web.Controllers
{
	public class FavoriteUserCountriesController : BaseController
	{
		// GET: FavoriteUserCountries
		public ActionResult Index()
		{
			var result = client.Get<List<FavoriteUserCountry>>(APIUrls.FavouriteUserCountries);
			return View(result.Data);
		}

		// GET: FavoriteUserCountries/Details/5
		public ActionResult Details(Guid? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var result = client.Get<FavoriteUserCountry>(APIUrls.FavouriteUserCountries);

			if (result.StatusCode != HttpStatusCode.OK)
			{
				return HttpNotFound();
			}

			return View(result);
		}

		// GET: FavoriteUserCountries/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: FavoriteUserCountries/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "UserId,CountryId,IsFavorite")] FavoriteUserCountry favoriteUserCountry)
		{
			if (ModelState.IsValid)
			{
				favoriteUserCountry.IsFavorite = true;
				var result = client.Post(APIUrls.CreateFavCountry, favoriteUserCountry);
				return RedirectToAction("Index");
			}

			return View("Index");
		}

		// GET: FavoriteUserCountries/Edit/5
		public ActionResult Edit(Guid id)
		{
			var result = client.Get<FavoriteUserCountry>(APIUrls.GetFevUserCountryById.Replace("{id}", id.ToString()));

			if (result.StatusCode != HttpStatusCode.OK)
			{
				return new HttpStatusCodeResult(result.StatusCode);
			}

			return View(result.Data);
		}

		// POST: FavoriteUserCountries/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,UserId,CountryId,IsFavorite")] FavoriteUserCountry favoriteUserCountry)
		{
			if (ModelState.IsValid)
			{
				client.Put(APIUrls.UpdateFevUserCountry.Replace("{id}", favoriteUserCountry.Id.ToString()), favoriteUserCountry);
				return RedirectToAction("Index");
			}

			return View(favoriteUserCountry);
		}

		// GET: FavoriteUserCountries/Delete/5
		public ActionResult Delete(Guid? id)
		{
			var result = client.Delete(APIUrls.GetFevUserCountryById.Replace("{id}", id.ToString()));
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			return View(new { });
		}

		// POST: FavoriteUserCountries/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(Guid id)
		{
			return RedirectToAction("Index");
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}
	}
}

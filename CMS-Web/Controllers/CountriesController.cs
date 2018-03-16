using CMS_Entity.Db;
using CMS_Entity.Models;
using CMS_Web.Constants;
using CMS_Web.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CMS_Web.Controllers
{
	public class CountriesController : BaseController
	{
		// GET: Countries
		public ActionResult Index()
		{
			var countriesResult = client.Get<List<Country>>(APIUrls.Countries);
			return BaseView<List<Country>>(countriesResult);
		}

		// GET: Countries
		public ActionResult Search()
		{
			var result = client.Get<List<SelectModel>>(APIUrls.CountryIdNames);
			var selectList = new SelectList(result.Data, "Id", "Name");

			SearchCountryViewModel searchCountry = new SearchCountryViewModel() { CountriesToSearch = selectList };
			return View(searchCountry);
		}

		// GET: Countries
		[HttpPost]
		public ActionResult Search(Guid countryId)
		{
			var countriesResult = client.Get<List<SelectModel>>(APIUrls.CountryIdNames);
			var countryResult = GetCountryById(countryId);
			var userResult = client.Get<CMS_Db.Entity.User>(APIUrls.LoggedInUser);
			var selectList = new SelectList(countriesResult.Data, "Id", "Name");
			var user = (CMS_Entity.Db.User)this.HttpContext.Cache[KeyConstants.User.ToString()];

			var fevCountry = countryResult.Data.FavoriteUserCountries.FirstOrDefault(c => user.Id.Equals(c.UserId));
			if (fevCountry == null)
			{
				fevCountry = new FavoriteUserCountry() { UserId = user.Id, CountryId = countryResult.Data.Id, IsFavorite = false };
			}

			SearchCountryViewModel searchCountry = new SearchCountryViewModel()
			{
				CountriesToSearch = selectList,
				Country = countryResult.Data,
				FevCountry = fevCountry,
			};

			return View(searchCountry);
		}

		[HttpPost]
		public ActionResult AddToFavorite()
		{
			var result = client.Get<List<SelectModel>>(APIUrls.CountryIdNames);
			return View(result.Data);
		}

		[HttpPost]
		public ActionResult RemoveFromFavorite()
		{
			var result = client.Get<List<SelectModel>>(APIUrls.CountryIdNames);
			return View(result.Data);
		}

		/// <summary>  
		/// This method will return PartialView with Employee Model  
		/// </summary>  
		/// <param name="EmployeeId"></param>  
		/// <returns></returns>  
		public PartialViewResult GetCountry(Guid id)
		{
			var result = client.Get<Country>(APIUrls.CountryIdNames);
			return PartialView("_CountryPartial", result.Data);
		}

		// GET: Countries/Details/5
		public ActionResult Details(Guid id)
		{
			var result = GetCountryById(id);
			return BaseView<Country>(result);
		}

		// GET: Countries/Edit/5
		public ActionResult Edit(Guid id)
		{
			var result = GetCountryById(id);
			return BaseView<Country>(result);
		}

		// POST: Countries/Edit/5
		[HttpPost]
		public ActionResult Edit(Guid id, Country country)
		{
			try
			{
				var result = client.Put(APIUrls.CountryById.Replace("{id}", id.ToString()), country);

				if (result.StatusCode != HttpStatusCode.OK)
				{
					return new HttpStatusCodeResult(result.StatusCode);
				}

				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}

		// GET: Countries/Delete/5
		public ActionResult Delete(Guid id)
		{
			var result = GetCountryById(id);
			return BaseView<Country>(result);
		}

		// POST: Countries/Delete/5
		[HttpPost]
		public ActionResult Delete(Guid id, FormCollection collection)
		{
			try
			{
				var result = client.Delete(APIUrls.CountryById.Replace("{id}", id.ToString()));

				if (result.StatusCode != HttpStatusCode.OK)
				{
					return new HttpStatusCodeResult(result.StatusCode);
				}

				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}

		#region Helpers

		private IRestResponse<Country> GetCountryById(Guid id)
		{
			return client.Get<Country>(APIUrls.CountryById.Replace("{id}", id.ToString()));
		}
		#endregion
	}
}

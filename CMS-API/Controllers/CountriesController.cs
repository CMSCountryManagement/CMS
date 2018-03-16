using Db = CMS_Entity.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using CMS_API.Models;
using AutoMapper;
using System.Web.Http.OData;
using CMS_Entity.Models;
using CountryAPI;
using CMS_Common;
using System.Data.Entity;

namespace CMS_API.Controllers
{
	public class CountriesController : BaseController
	{
		private readonly ICountryManager _countryManager;

		public CountriesController(ICountryManager countryManager)
		{
			_countryManager = countryManager;
		}

		[HttpGet]
		[Route("~/api/ImportCounries")]
		public IHttpActionResult ImportCountries()
		{
			var countries = _countryManager.GetCountries<Country>(CountryApiConstants.AllCountries);
			IList<Db.Country> countriesToSave = Mapper.Map<IList<Country>, IList<Db.Country>>(countries);

			if (Repository.Countries.Any())
			{
				Repository.Countries.AddRange(countriesToSave);
				Repository.SaveChanges();
				return Ok(Repository.Countries);
			}

			return Ok(countries);
		}

		[HttpGet]
		[Route("~/api/Country/IdNames")]
		public IEnumerable<SelectModel> CountryIdNames()
		{
			return Repository.Countries.Select(c => new SelectModel { Id = c.Id, Name = c.Name });
		}

		// GET: api/Countries
		//[EnableQuery]
		[HttpGet]
		[Queryable(PageSize = 10)]
		public PageResult<Db.Country> Get()
		{
			IQueryable results = Repository.Countries;

			return new PageResult<Db.Country>(
				results as IEnumerable<Db.Country>, Request.RequestUri, 10);
		}

		// GET: api/Countries/5
		public IHttpActionResult Get(Guid id)
		{
			var country = Repository.Countries.Find(id);

			return Ok(country);
		}

		// POST: api/Countries
		public IHttpActionResult Post([FromBody]string value)
		{
			return Ok();
		}

		// PUT: api/Countries/5
		public IHttpActionResult Put(Guid id, Db.Country country)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			//var countryToUpdate = Repository.Countries.Find(id);
			//if (countryToUpdate == null)
			//{
			//	return NotFound();
			//}

			Repository.Entry(country).State = EntityState.Modified;
			Repository.SaveChanges();

			return Ok(country);
		}

		// DELETE: api/FavoriteUserCountry/5
		public IHttpActionResult Delete(Guid id)
		{
			var country = Repository.Countries.Find(id);

			if (country == null)
			{
				return NotFound();
			}

			Repository.Countries.Remove(country);
			Repository.SaveChanges();

			return Ok();
		}
	}
}

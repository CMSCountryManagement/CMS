using CMS_Entity.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using System.Data.Entity.Infrastructure;
using AutoMapper;
using CMS_Common;
using System.Data.Entity;

namespace CMS_API.Controllers
{
    public class FavoriteUserCountryController : BaseController
    {
        // GET: api/FavoriteUserCountry
        [Queryable]
        public IQueryable<FavoriteUserCountry> Get()
        {
            var userId = RequestContext.Principal.Identity.GetUserId();
            var user = Repository.Users.Find(userId);
            var roles = Repository.Roles;

            if (user.UserRoles.Any(r => r.Role.Name.Equals(RoleConstant.Administrator.ToString())))
            {
                return Repository.FavoriteUserCountries;
            }
            else
            {
                return Repository.FavoriteUserCountries.Where(c => c.UserId.Equals(userId, StringComparison.InvariantCultureIgnoreCase)).Include(c => c.Country);
            }
        }

        // GET: api/FavoriteUserCountry/5
        public IHttpActionResult Get(Guid id)
        {
			//return Ok(Repository.FavoriteUserCountries.Where(c => c.Id == id).Include(c => c.Country).Include(c => c.User).FirstOrDefault());
            return Ok(Repository.FavoriteUserCountries.Find(id));
        }

        // POST: api/FavoriteUserCountry
        public IHttpActionResult Post(FavoriteUserCountry favoriteUserCountry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            favoriteUserCountry.Id = Guid.NewGuid();
            Repository.FavoriteUserCountries.Add(favoriteUserCountry);
            Repository.SaveChanges();

            return Ok(favoriteUserCountry); ;
        }

        // PUT: api/FavoriteUserCountry/5
        public IHttpActionResult Put(Guid id, FavoriteUserCountry favoriteUserCountry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var favoriteUserCountryToUpdate = Repository.FavoriteUserCountries.Find(id);
            if (favoriteUserCountryToUpdate == null)
            {
                return NotFound();
            }

            favoriteUserCountryToUpdate.IsFavorite = favoriteUserCountry.IsFavorite;
            //Repository.Entry(favoriteUserCountryToUpdate).State = EntityState.Modified;
            Repository.SaveChanges();

            return Ok(favoriteUserCountry);
        }

        // DELETE: api/FavoriteUserCountry/5
        public IHttpActionResult Delete(Guid id)
        {
            var favoriteUserCountryToUpdate = Repository.FavoriteUserCountries.Find(id);

            if (favoriteUserCountryToUpdate == null)
            {
                return NotFound();
            }

            Repository.FavoriteUserCountries.Remove(favoriteUserCountryToUpdate);
            Repository.SaveChanges();

            return Ok();
        }
    }
}

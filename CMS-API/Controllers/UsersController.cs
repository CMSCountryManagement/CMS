using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CMS_Entity.Db;
using Microsoft.AspNet.Identity;

namespace CMS_API.Controllers
{
    public class UsersController : BaseController
    {
        // GET: api/Users
        public IEnumerable<User> GetIdentityUsers()
        {
            return Repository.Users;
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(string id)
        {
            User user = Repository.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

		// GET: api/Users/5
		[Route("~/api/Users/ByUserName")]
		[ResponseType(typeof(User))]
		public IHttpActionResult GetUserByUserName(string userName)
		{
			User user = Repository.Users.FirstOrDefault(u => u.Email.Equals(userName));
			if (user == null)
			{
				return NotFound();
			}

			return Ok(user);
		}

		[HttpGet]
		[Route("~/api/Users/LoggedInUser")]
		[ResponseType(typeof(User))]
		public IHttpActionResult GetLoggedInUser()
		{
			var userId = RequestContext.Principal.Identity.GetUserId();
            User user = Repository.Users.Find(userId);
            User user2 = Repository.Users.Include(u => u.FavoriteUserCountries).FirstOrDefault(u => u.Id.Equals(userId));
            
            if (user == null)
			{
				return NotFound();
			}

            return Ok(user2);
		}

		// PUT: api/Users/5
		[ResponseType(typeof(void))]
        public IHttpActionResult PutUser(string id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            Repository.Entry(user).State = EntityState.Modified;

            try
            {
                Repository.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Repository.Users.Add(user);

            try
            {
                Repository.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(string id)
        {
            User user = Repository.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            Repository.Users.Remove(user);
            Repository.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Repository.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(string id)
        {
            return Repository.Users.Count(e => e.Id == id) > 0;
        }
    }
}
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using CMS_Entity.Db;

namespace CMS_API.Controllers
{
    public class LogsController : BaseController
    {
        // GET: api/Logs
        public IQueryable<Log> GetLogs()
        {
            return Repository.Logs;
        }

        // GET: api/Logs/5
        [ResponseType(typeof(Log))]
        public IHttpActionResult GetLog(Guid id)
        {
            Log log = Repository.Logs.Find(id);
            if (log == null)
            {
                return NotFound();
            }

            return Ok(log);
        }

        // PUT: api/Logs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLog(Guid id, Log log)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != log.Id)
            {
                return BadRequest();
            }

            Repository.Entry(log).State = EntityState.Modified;

            try
            {
                Repository.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LogExists(id))
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

        // POST: api/Logs
        [ResponseType(typeof(Log))]
        public IHttpActionResult PostLog(Log log)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Repository.Logs.Add(log);

            try
            {
                Repository.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (LogExists(log.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = log.Id }, log);
        }

        // DELETE: api/Logs/5
        [ResponseType(typeof(Log))]
        public IHttpActionResult DeleteLog(Guid id)
        {
            Log log = Repository.Logs.Find(id);
            if (log == null)
            {
                return NotFound();
            }

            Repository.Logs.Remove(log);
            Repository.SaveChanges();

            return Ok(log);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Repository.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LogExists(Guid id)
        {
            return Repository.Logs.Count(e => e.Id == id) > 0;
        }
    }
}
using CMS_Common;
using CMS_Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CMS_API.Controllers
{
    public class ReportController : BaseController
    {
        // GET: api/Report
        public IEnumerable<string> Get()
        {
            return null;
            //return dbContext.Logs.Where(l => l.Activity.Equals(LogActivitiyConstants.SearchCountry.ToString())).    Where(c => c.UserId.Equals(RequestContext.Principal.Identity.GetUserId(), StringComparison.InvariantCultureIgnoreCase)).ToList();
        }

        // GET: api/Report/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Report
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Report/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Report/5
        public void Delete(int id)
        {
        }
    }
}

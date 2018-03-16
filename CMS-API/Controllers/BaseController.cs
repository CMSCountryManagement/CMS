using CMS_Entity.Db;
using System;
using System.Web.Http;

namespace CMS_API.Controllers
{
	[Authorize]
    public class BaseController : ApiController
    {
        public static Func<CMSDbModel> RepositoryFactory = () => new CMSDbModel();

        protected CMSDbModel Repository { get; private set; }

        public BaseController()
        {
            Repository = RepositoryFactory();
        }

        protected override void Dispose(bool disposing)
        {
            Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}

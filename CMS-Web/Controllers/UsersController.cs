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
	public class UsersController : EntityController<User>
	{
		public UsersController()
			: base(APIUrls.Users)
		{
		}
	}
}

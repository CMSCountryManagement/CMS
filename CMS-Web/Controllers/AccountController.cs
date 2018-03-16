using CMS_Db.Entity;
using CMS_Web.Client;
using CMS_Web.Constants;
using CMS_Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using System.Web.Security;

namespace CMS_Web.Controllers
{
	[Authorize]
	public class AccountController : Controller
	{
		public AccountController()
		{
		}

		//
		// GET: /Account/Login
		[AllowAnonymous]
		public ActionResult Login(string returnUrl)
		{
			ViewBag.ReturnUrl = returnUrl;
			return View();
		}

		//
		// POST: /Account/Login
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			CMSApiClient client = new CMSApiClient(HttpContext, model.Email, model.Password);
			var result = client.GetToken();

			if (result.StatusCode == HttpStatusCode.OK)
			{
				var userResult = client.Get<CMS_Entity.Db.User>(APIUrls.LoggedInUser);
				var user = userResult.Data;
				CreateCookie(user);

				this.HttpContext.Cache.Insert(KeyConstants.User.ToString(), user, null,
						DateTime.UtcNow.AddSeconds(60 * 60), Cache.NoSlidingExpiration);

				return RedirectToLocal(returnUrl);
			}
			else
			{
				ModelState.AddModelError("", "Invalid login attempt.");
				return View(model);
			}
		}

		//
		// GET: /Account/Register
		[AllowAnonymous]
		public ActionResult Register()
		{
			return View();
		}

		//
		// POST: /Account/Register
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = new ApplicationUser { UserName = model.Email, Email = model.Email };

				CMSApiClient client = new CMSApiClient(HttpContext);
				var result = client.Post(APIUrls.Register, model, true);

				if (result.StatusCode == HttpStatusCode.OK)
				{
					//await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

					return RedirectToAction("Login");
				}
			}

			return View(model);
		}

		// GET: /Account/ResetPassword
		[AllowAnonymous]
		public ActionResult ResetPassword(string code)
		{
			return code == null ? View("Error") : View();
		}

		//
		// POST: /Account/ResetPassword
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			CMSApiClient client = new CMSApiClient(HttpContext);
			var result = client.Post(APIUrls.ChangePassword, model);

			if (result.StatusCode == HttpStatusCode.OK)
			{
				return RedirectToAction("ResetPasswordConfirmation", "Account");
			}

			return View();
		}

		//
		// GET: /Account/ResetPasswordConfirmation
		[AllowAnonymous]
		public ActionResult ResetPasswordConfirmation()
		{
			return View();
		}

		//
		// POST: /Account/LogOff
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult LogOff()
		{
			AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
			FormsAuthentication.SignOut();
			CMSApiClient client = new CMSApiClient(HttpContext);
			client.LogOut();
			return RedirectToAction("Login", "Account");
		}

		//
		// GET: /Account/ExternalLoginFailure
		[AllowAnonymous]
		public ActionResult ExternalLoginFailure()
		{
			return View();
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		#region Helpers
		// Used for XSRF protection when adding external logins
		private const string XsrfKey = "XsrfId";

		private IAuthenticationManager AuthenticationManager
		{
			get
			{
				return HttpContext.GetOwinContext().Authentication;
			}
		}

		private ActionResult RedirectToLocal(string returnUrl)
		{
			if (Url.IsLocalUrl(returnUrl))
			{
				return Redirect(returnUrl);
			}
			return RedirectToAction("Index", "Home");
		}

		internal class ChallengeResult : HttpUnauthorizedResult
		{
			public ChallengeResult(string provider, string redirectUri)
				: this(provider, redirectUri, null)
			{
			}

			public ChallengeResult(string provider, string redirectUri, string userId)
			{
				LoginProvider = provider;
				RedirectUri = redirectUri;
				UserId = userId;
			}

			public string LoginProvider { get; set; }
			public string RedirectUri { get; set; }
			public string UserId { get; set; }

			public override void ExecuteResult(ControllerContext context)
			{
				var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
				if (UserId != null)
				{
					properties.Dictionary[XsrfKey] = UserId;
				}
				context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
			}
		}

		public void CreateCookie(CMS_Entity.Db.User user)
		{
			HttpCookie ck;
			FormsAuthenticationTicket tkt;
			string profileImage = string.Empty;
			string userdata = string.Empty;
			string cookiestr;

			profileImage = "/AdminTemplete/dist/img/user2-160x160.jpg";
			userdata = string.Concat(user.Id, "|", user.UserName, "|", user.Email, "|", profileImage, "|", String.Join(",", user.UserRoles.Select(r => r.Role.Name)));
			tkt = new FormsAuthenticationTicket(1, user.UserName, DateTime.Now, DateTime.Now.AddMinutes(60), false, userdata);
			cookiestr = FormsAuthentication.Encrypt(tkt);
			ck = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr);
			ck.Path = FormsAuthentication.FormsCookiePath;
			System.Web.HttpContext.Current.Response.Cookies.Add(ck);
			Response.Cookies["'" + user.UserName + "'"]["ProfileId"] = user.UserName;
		}
		#endregion
	}
}
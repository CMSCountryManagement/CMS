using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using RestSharp;
using RestSharp.Authenticators;
using System.Web;
using System.Net;
using System.Web.Caching;
using Newtonsoft.Json;
using CMS_Db.Entity;

namespace CMS_API_Client
{
	public class CMSApiClient
	{
		private string _apiLocation = "http://localhost:62721/"; // The api location goes here
        private string _username = "mahesh2018@gmail.com";    // The api username goes here
        private string _password = "Mahesh@2018";    // The api username goes here

		public HttpContextBase HttpContext { get; private set; }

		public CMSApiClient(HttpContextBase httpContext)
		{
			this.HttpContext = httpContext;
		}

		public CMSApiClient(string apiLocation, string username, string password)
		{
			_apiLocation = apiLocation;
			_username = username;
			_password = password;
		}

		/// <summary>
		/// Gets authentication token from the API
		/// <remarks>Gets from cach, if expired then re-authenticate with api and set to cache.</remarks>
		/// </summary>
		/// <returns>
		/// The token.
		/// </returns>
		private string GetAuthenticationToken()
		{
			var token = (ApiAuthenticationResponse)this.HttpContext.Cache["ApiAuthenticationToken"];

			if (token == null)
			{
				var client = new RestClient(this._apiLocation)
				{
					Authenticator = new HttpBasicAuthenticator(this._username, this._password)
				};

				var request = new RestRequest("token", Method.POST)
				{
					RequestFormat = DataFormat.Json
				};

                string encodedBody = String.Format("grant_type=password&username={0}&password={1}", _username, _password);
				request.AddParameter("application/x-www-form-urlencoded", encodedBody, ParameterType.RequestBody);

				var response = client.Execute<ApiAuthenticationResponse>(request);

				if (response.StatusCode == HttpStatusCode.OK)
				{
					token = response.Data;

					this.HttpContext.Cache.Insert("ApiAuthenticationToken", token, null,
						DateTime.UtcNow.AddSeconds(response.Data.expires_in), Cache.NoSlidingExpiration);

					//var user = Get<User>("/api/Users/LoggedInUser");

					//this.HttpContext.Cache.Insert("User", user, null,
					//	DateTime.UtcNow.AddSeconds(response.Data.expires_in), Cache.NoSlidingExpiration);
				}
				else
				{
					//throw response.ErrorException;
				}
			}

			return token.access_token;
		}

		/// <summary>
		/// The implementation of the contact form model.
		/// </summary>
		public class ContactFormModel
		{
			/// <summary>
			/// The first name.
			/// </summary>
			public string FirstName { get; set; }

			/// <summary>
			/// The last name.
			/// </summary>
			public string LastName { get; set; }

			/// <summary>
			/// The email address.
			/// </summary>
			public string Email { get; set; }

			/// <summary>
			/// The phone.
			/// </summary>
			public string Phone { get; set; }

			/// <summary>
			/// The message.
			/// </summary>
			public string Message { get; set; }
		}

		public IRestResponse<T> Get<T>(string url) where T : new()
		{
			var client = new RestClient(this._apiLocation);
			var request = new RestRequest(url, Method.GET)
			{
				RequestFormat = DataFormat.Json
			};

			request.AddHeader("Authorization", string.Format("bearer {0}", this.GetAuthenticationToken()));
			request.AddHeader("Accept", "application/json");

			var result = client.Execute<T>(request);

			if (result.StatusCode != HttpStatusCode.OK)
			{
				throw result.ErrorException;
			}

			return result;
		}

		public void Put(ContactFormModel model)
		{
			var client = new RestClient(this._apiLocation);
			var request = new RestRequest("contactUs", Method.POST)
			{
				RequestFormat = DataFormat.Json
			};

			request.AddHeader("Authorization", string.Format("bearer {0}", this.GetAuthenticationToken()));
			request.AddHeader("Accept", "application/json");
			request.AddParameter("application/json", JsonConvert.SerializeObject(model), ParameterType.RequestBody);

			var result = client.Execute(request);

			if (result.StatusCode != HttpStatusCode.OK)
			{
				throw result.ErrorException;
			}
		}

		public void Post(ContactFormModel model)
		{
			var client = new RestClient(this._apiLocation);
			var request = new RestRequest("contactUs", Method.POST)
			{
				RequestFormat = DataFormat.Json
			};

			request.AddHeader("Authorization", string.Format("bearer {0}", this.GetAuthenticationToken()));
			request.AddHeader("Accept", "application/json");
			request.AddParameter("application/json", JsonConvert.SerializeObject(model), ParameterType.RequestBody);

			var result = client.Execute(request);

			if (result.StatusCode != HttpStatusCode.OK)
			{
				throw result.ErrorException;
			}
		}

		public void Delete(ContactFormModel model)
		{
			var client = new RestClient(this._apiLocation);
			var request = new RestRequest("contactUs", Method.POST)
			{
				RequestFormat = DataFormat.Json
			};

			request.AddHeader("Authorization", string.Format("bearer {0}", this.GetAuthenticationToken()));
			request.AddHeader("Accept", "application/json");
			request.AddParameter("application/json", JsonConvert.SerializeObject(model), ParameterType.RequestBody);

			var result = client.Execute(request);

			if (result.StatusCode != HttpStatusCode.OK)
			{
				throw result.ErrorException;
			}
		}
	}
}

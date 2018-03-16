using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using System.Web;
using System.Net;
using System.Web.Caching;
using Newtonsoft.Json;
using CMS_Entity.Db;
using System.Web.Routing;
using System.Web.Mvc;
using System.Configuration;
using CMS_Web.Constants;

namespace CMS_Web.Client
{
    public class CMSApiClient
    {
        private string _apiLocation = ConfigurationManager.AppSettings["WebApiUrl"];
        private string _username = "";    // The api username goes here
        private string _password = "";    // The api username goes here

        public HttpContextBase HttpContext { get; private set; }

        public CMSApiClient(HttpContextBase httpContext)
        {
            this.HttpContext = httpContext;
        }

        public CMSApiClient(HttpContextBase httpContext, string username, string password)
        {
            this.HttpContext = httpContext;
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
        public string GetAuthenticationToken()
        {
            var token = (ApiAuthenticationResponse)this.HttpContext.Cache["ApiAuthenticationToken"];

            if (token == null)
            {
                if (String.IsNullOrEmpty(_username) || String.IsNullOrEmpty(_password))
                {
                    this.HttpContext.Response.Redirect("~/Account/Login");
                    return null;
                }
                else
                {
                    var response = GetToken();
                    token = response.Data;
                }
            }

            return token.access_token;
        }

        public IRestResponse<ApiAuthenticationResponse> GetToken()
        {
            var client = new RestClient(this._apiLocation)
            {
                Authenticator = new HttpBasicAuthenticator(this._username, this._password)
            };

            var request = new RestRequest(APIUrls.token, Method.POST)
            {
                RequestFormat = DataFormat.Json
            };

            string encodedBody = String.Format("grant_type=password&username={0}&password={1}", _username, _password);
            request.AddParameter("application/x-www-form-urlencoded", encodedBody, ParameterType.RequestBody);

            var response = client.Execute<ApiAuthenticationResponse>(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var token = response.Data;

                this.HttpContext.Cache.Insert(KeyConstants.ApiAuthenticationToken.ToString(), token, null,
                    DateTime.UtcNow.AddSeconds(response.Data.expires_in), Cache.NoSlidingExpiration);

                //var user = Get<User>("/api/Users/LoggedInUser");

                //this.HttpContext.Cache.Insert("User", user, null,
                //	DateTime.UtcNow.AddSeconds(response.Data.expires_in), Cache.NoSlidingExpiration);
            }
            else
            {
                //throw response.ErrorException;
            }

            return response;
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

            if (result.Data == null)
            {
                result.Data = JsonConvert.DeserializeObject<T>(result.Content);
            }

            if (result.StatusCode != HttpStatusCode.OK)
            {
                throw result.ErrorException;
            }

            return result;
        }

        public IRestResponse<T> Post<T>(string url, object model, bool allowAnonymous = false) where T : new()
        {
            var client = new RestClient(this._apiLocation);
            var request = new RestRequest(url, Method.POST)
            {
                RequestFormat = DataFormat.Json
            };

            if (!allowAnonymous)
            {
                request.AddHeader("Authorization", string.Format("bearer {0}", this.GetAuthenticationToken()));
            }

            request.AddHeader("Accept", "application/json");
            request.AddParameter("application/json", JsonConvert.SerializeObject(model), ParameterType.RequestBody);

            var result = client.Execute<T>(request);

            if (result.StatusCode != HttpStatusCode.OK)
            {
                throw result.ErrorException;
            }

            return result;
        }

        public IRestResponse Put(string url, object model)
        {
            var client = new RestClient(this._apiLocation);
            var request = new RestRequest(url, Method.PUT)
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

            return result;
        }


        public IRestResponse Delete(string url, object model = null)
        {
            var client = new RestClient(this._apiLocation);
            var request = new RestRequest(url, Method.DELETE)
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

            return result;
        }

        internal IRestResponse Post(string url, object model, bool allowAnonymous = false)
        {
            var client = new RestClient(this._apiLocation);
            var request = new RestRequest(url, Method.POST)
            {
                RequestFormat = DataFormat.Json
            };

            if (!allowAnonymous)
            {
                request.AddHeader("Authorization", string.Format("bearer {0}", this.GetAuthenticationToken()));
            }

            request.AddHeader("Accept", "application/json");
            request.AddParameter("application/json", JsonConvert.SerializeObject(model), ParameterType.RequestBody);

            var result = client.Execute(request);

            if (result.StatusCode != HttpStatusCode.OK)
            {
                throw result.ErrorException;
            }

            return result;
        }

        public void LogOut()
        {
            HttpContext.Cache.Remove(KeyConstants.ApiAuthenticationToken.ToString());
        }
    }
}
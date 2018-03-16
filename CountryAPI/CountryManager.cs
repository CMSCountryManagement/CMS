using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;

namespace CountryAPI
{
	public class CountryManager : ICountryManager
	{
		private static readonly string countryUrl = ConfigurationManager.AppSettings["CountryUrl"];

		public IList<T> GetCountries<T>(string additionalUrl) where T : new()
		{
			IList<T> countries = null;
			var responseText = GetResponse(countryUrl + additionalUrl);
			countries = JsonConvert.DeserializeObject<IList<T>>(responseText);

			return countries;
		}

		public T GetCountryByName<T>(string name) where T : new()
		{
			throw new NotImplementedException();
		}

		public T GetCountryByFullName<T>(string fullName) where T : new()
		{
			throw new NotImplementedException();
		}

		public T GetCountryByCode<T>(string code) where T : new()
		{
			throw new NotImplementedException();
		}

		public string GetResponse(string url)
		{
			string responseText = string.Empty;
			// Creates an HttpWebRequest for the specified URL. 
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			// Sends the HttpWebRequest and waits for a response.
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();

			if (response.StatusCode == HttpStatusCode.OK)
			{
				using (var read = new System.IO.StreamReader(response.GetResponseStream()))
				{
					responseText = read.ReadToEnd();
				}
			}

			response.Close();

			return responseText;
		}
	}
}
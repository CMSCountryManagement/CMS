using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_Web.Constants
{
    public static class APIUrls
    {
        public const string token = "token";
		public const string Users = "api/Users";
		public const string Countries = "api/Countries";
		public const string CountryIdNames = "api/Country/IdNames";
        public const string CountryById = "api/Countries/{id} ";
        public const string FavouriteUserCountries = "api/FavoriteUserCountry";
        public const string LoggedInUser = "/api/Users/LoggedInUser";
		public const string UpdateFevUserCountry = "api/FavoriteUserCountry/{id}";
		public const string GetFevUserCountryById = "api/FavoriteUserCountry/{id}";
        public const string CreateFavCountry = "api/FavoriteUserCountry";
		public const string ChangePassword = "/api/Account/ChangePassword";
		public const string Register = "/api/Account/Register";
	}
}
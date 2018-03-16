using System;
using System.Collections.Generic;
using System.Linq;
using CMS_Entity.Models;
using CMS_Entity.Db;
using System.Web;
using System.Web.Mvc;

namespace CMS_Web.Models
{
    public class SearchCountryViewModel
    {
        public SelectList CountriesToSearch { get; set; }
        public Country Country { get; set; }
        public FavoriteUserCountry FevCountry { get; set; }
    }
}
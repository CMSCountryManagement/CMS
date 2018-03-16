using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_API.Models
{
    public class ApiLogging
    {
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string Ip { get; set; }
        public string Parameters { get; set; }
        public string DataParameter { get; set; }
        public string RequestUrl { get; set; }
    }
}
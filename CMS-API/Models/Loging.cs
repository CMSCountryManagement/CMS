
using CMS_Db;
using CMS_Db.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;

namespace CMS_API.Models
{
    public class ApiEvent : HttpApplication
    {
        private readonly CMSDbContext dbContext = new CMSDbContext();

        public async Task InsertLogAsync(List<ApiLogging> objaccess)
        {
            try
            {
                Log log = new Log();

                foreach (var item in objaccess)
                {
                    log.ControllerName = item.ControllerName;
                    log.ActionName = item.ActionName;
                    log.Ip = item.Ip;
                    log.CreatedOn = DateTime.Now;
                    log.Parameter = item.Parameters;
                    log.RequestUrl = item.RequestUrl;
                }

                //dbContext.Logs.Add(log);
                //dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                string errormsg = ex.Message.ToString();
            }
        }

        public void Logging()
        {
            string action = "";
            string controller = "";
            string parameter = "";
            string hostName = "";
            string ipaddress = "";
            string requesturl = "";
            string dataparameter = String.Empty;

            List<ApiLogging> logs = new List<ApiLogging>();
            ApiEvent objevent = new ApiEvent();
            HttpContextBase currentContext = new HttpContextWrapper(HttpContext.Current);
            RouteData routeData = HttpContext.Current.Request.RequestContext.RouteData;

            action = routeData.Values["action"] == null ? null : routeData.Values["action"].ToString();
            controller = routeData.Values["controller"] == null ? null : routeData.Values["controller"].ToString();
            parameter = routeData.Values["id"] == null ? null : routeData.Values["id"].ToString();
            hostName = Dns.GetHostName();
            ipaddress = Dns.GetHostEntry(hostName).AddressList[0].ToString();
            HttpContext.Current.Request.InputStream.Position = 0;

            using (StreamReader inputStream = new StreamReader(HttpContext.Current.Request.InputStream))
            {
                dataparameter = inputStream.ReadToEnd();
            }

            requesturl = HttpContext.Current.Request.Url.AbsoluteUri;
            logs.Add(new ApiLogging { ControllerName = controller, ActionName = action, Ip = ipaddress, Parameters = parameter, DataParameter = dataparameter, RequestUrl = requesturl });
            objevent.InsertLogAsync(logs);
        }
    }
}
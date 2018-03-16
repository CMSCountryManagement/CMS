using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_API.Models
{
    public class Currency
    {
        public Guid Id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string symbol { get; set; }
    }
}

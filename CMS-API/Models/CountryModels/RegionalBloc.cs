using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_API.Models
{
    public class RegionalBloc
    {
        public Guid Id { get; set; }
        public string acronym { get; set; }
        public string name { get; set; }
        public List<object> otherAcronyms { get; set; }
        public List<string> otherNames { get; set; }
    }
}

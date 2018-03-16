using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountryAPI
{
    public interface ICountryManager
    {
        IList<T> GetCountries<T>(string additionalUrl) where T : new();
		T GetCountryByName<T>(string name) where T : new();
		T GetCountryByFullName<T>(string fullName) where T : new();
		T GetCountryByCode<T>(string code) where T : new();
    }
}

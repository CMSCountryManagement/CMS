using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS_Db.Services
{
    public interface IBaseService
    {
        T Get<T>() where T : new();
        T Get<T>(string id) where T : new();
        T GetByName<T>(string name) where T : new();
        void Create<T>(T name) where T : new();
        void Update<T>(T name) where T : new();
        void Delete<T>(T name) where T : new();
    }
}

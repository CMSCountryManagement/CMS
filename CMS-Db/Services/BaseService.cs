using CMS_Entity.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS_Db.Services
{
    public class BaseService : IBaseService, IDisposable
    {
        public static Func<CMSDbModel> RepositoryFactory = () => new CMSDbModel();

        protected CMSDbModel Repository { get; private set; }

        public BaseService()
        {
            Repository = RepositoryFactory();
        }

        public T Get<T>() where T : new()
        {
            throw new NotImplementedException();
        }

        public T Get<T>(string id) where T : new()
        {
            throw new NotImplementedException();
        }

        public T GetByName<T>(string name) where T : new()
        {
            throw new NotImplementedException();
        }

        public void Create<T>(T name) where T : new()
        {
            throw new NotImplementedException();
        }

        public void Update<T>(T name) where T : new()
        {
            throw new NotImplementedException();
        }

        public void Delete<T>(T name) where T : new()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Repository.Dispose();
        }
    }
}

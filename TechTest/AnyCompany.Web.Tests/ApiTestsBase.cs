using System;
using AnyCompany.Service.Cache;
using AnyCompany.Service.Config;
using AnyCompany.Service.Container;
using AnyCompany.Service.DAL;
using AnyCompany.Service.Logger;

namespace AnyCompany.Web.Tests
{
    public class ApiTestsBase
    {
        protected IConfig _testConfig = new DefaultConfig();
        
        protected IDatabaseWrapper _databaseWrapper;
        protected readonly DefaultContainer _container;

        public ApiTestsBase()
        { 
            _testConfig = new DefaultConfig();
            
            _databaseWrapper = new RelationalDatabaseWrapper(_testConfig.DATABASE_URL, new SimpleInMemoryCache(), _testConfig.ROOT_QUERIES_DIR, new StreamLogger(Console.OpenStandardOutput()));

            _container = new DefaultContainer(_testConfig, null, _databaseWrapper);
            // Set up mock data in the test database, in order to test flows correctness.
        }
    }
}
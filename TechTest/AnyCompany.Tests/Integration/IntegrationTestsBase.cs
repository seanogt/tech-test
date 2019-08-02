using System;
using AnyCompany.Service.Cache;
using AnyCompany.Service.Config;
using AnyCompany.Service.DAL;
using AnyCompany.Service.Logger;

namespace AnyCompany.Tests.Integration
{
    public class IntegrationTestsBase
    {
        protected IConfig _testConfig = new DefaultConfig();
        
        protected IDatabaseWrapper _databaseWrapper;

        public IntegrationTestsBase()
        { 
            // Read from .env.test file, or just use the regular env if running in a docker container.
            _testConfig = new DefaultConfig();
            
            _databaseWrapper = new RelationalDatabaseWrapper(_testConfig.DATABASE_URL, new SimpleInMemoryCache(), _testConfig.ROOT_QUERIES_DIR, new StdOutLogger(Console.OpenStandardOutput()));
        }
    }
}
using System;
using System.Net.Sockets;
using AnyCompany.Service.Cache;
using AnyCompany.Service.Config;
using AnyCompany.Service.DAL;
using AnyCompany.Service.DAL.DataManagers;
using AnyCompany.Service.Facades;
using AnyCompany.Service.Logger;

namespace AnyCompany.Service.Container
{
    /// </summary>
    public class DefaultContainer : IContainer
    {

        /// <summary>
        /// Dependency injection container for all the system's entities
        /// </summary>
        /// <param name="config">Configuration provider, defaults to DefaultConfig.cs</param>
        /// <param name="cache">Cache wrapper, defaults to SimpleInMemoryCache</param>
        /// <param name="dbWrapper">DB Wrapper, especially useful for tests. Defauls to RelationalDatabaseWrapper</param>
        /// <param name="customersFacade">A facade to customers data. Defaults to CustomersDbManager</param>
        /// <param name="taxesFacade">A facade to taxes data. Defaults to TaxesDbManager</param>
        /// <param name="ordersFacade">A facade to orders data. Defaults to OrdersDbManager</param>
        public DefaultContainer(
            IConfig config = null,
            IKeyValueCache cache = null,
            IDatabaseWrapper dbWrapper = null,
            ICustomerFacade customersFacade = null,
            ITaxesFacade taxesFacade = null,
            IOrdersFacade ordersFacade = null)
        {
            Config = config ?? new DefaultConfig(); 
            Logger = new StdOutLogger(Console.OpenStandardInput());
            Cache = cache ?? new SimpleInMemoryCache(); 
            
            DbWrapper = dbWrapper ?? new RelationalDatabaseWrapper(Config.DATABASE_URL, Cache, Config.ROOT_QUERIES_DIR, Logger);

            CustomersFacade = customersFacade ?? new CustomersDbManager(DbWrapper);
            TaxesFacade = taxesFacade ?? new TaxesDbManager(DbWrapper);
            OrdersFacade = ordersFacade ?? new OrdersDbManager(DbWrapper);
        }
        
        public IKeyValueCache Cache { get; private set;}
        public IConfig Config{ get; private set;}
        public ILogger Logger{ get; private set;}
        
        public IDatabaseWrapper DbWrapper { get; private set;}
        public ICustomerFacade CustomersFacade { get; private set;}
        public ITaxesFacade TaxesFacade { get; private set;}
        public IOrdersFacade OrdersFacade { get; private set;}
        
    }
}
using AnyCompany.Service.Cache;
using AnyCompany.Service.Config;
using AnyCompany.Service.DAL;
using AnyCompany.Service.Facades;
using AnyCompany.Service.Logger;

namespace AnyCompany.Service.Container
{
    /// <summary>
    /// DI container for all dependencies
    /// </summary>
    public interface IContainer
    {
        /// <summary>
        /// Default cache. Will be used by the DB wrapper for caching queryes, etc.
        /// </summary>
        IKeyValueCache Cache { get; } 
        /// <summary>
        /// Configuration provider, which contains keys like DATABASE_URL, etc.
        /// </summary>
        IConfig Config{ get; } 
        /// <summary>
        /// Logger. Can be a regular console stream logger, or a file stream (i.e AWS S3 monitored file).
        /// </summary>
        ILogger Logger{ get; } 
        /// <summary>
        /// One instance for the DB wrapper to be used across the program
        /// </summary>
        IDatabaseWrapper DbWrapper { get; }
        
        
        // Business Logic:
        
        ICustomerFacade CustomersFacade { get; }
        ITaxesFacade TaxesFacade { get; }
        IOrdersFacade OrdersFacade { get; }
        
    }
}
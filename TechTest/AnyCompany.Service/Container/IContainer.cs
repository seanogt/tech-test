using AnyCompany.Service.DAL;
using AnyCompany.Service.Facades;

namespace AnyCompany.Service.Container
{
    /// <summary>
    /// DI container for all dependencies
    /// </summary>
    public interface IContainer
    {
        IDatabaseWrapper DbWrapper { get; }
        ICustomerFacade CustomersFacade { get; }
        ITaxesFacade TaxesFacade { get; }
        IOrdersFacade OrdersFacade { get; }
        
    }
}
using System.Threading.Tasks;
using AnyCompany.Service.Models;

namespace AnyCompany.Service.Facades
{
    /// <summary>
    /// Describes functionality for retrieving and managing customers.
    /// This is implementation-agnostic, the implementor can be a local DB provider as well as
    /// an external service connector.
    /// </summary>
    public interface ICustomerFacade
    {
        /// <summary>
        /// Retrieves the customer by customerId.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        Task<Customer> GetCustomerById(string customerId);
        
        /// <summary>
        /// Creates a customer, and returns its ID if successful. 
        /// </summary>
        /// <param name="customer">Customer to create</param>
        /// <returns>New customer ID</returns>
        Task<string> CreateCustomer(Customer customer);
    }
}
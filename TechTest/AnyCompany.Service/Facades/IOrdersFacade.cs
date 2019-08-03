using System.Collections.Generic;
using System.Threading.Tasks;
using AnyCompany.Service.Models;

namespace AnyCompany.Service.Facades
{
    /// <summary>
    /// Facade to manage orders
    /// </summary>
    public interface IOrdersFacade 
    {
        /// <summary>
        /// Returns an order by id, throws if not found
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        Task<Order> GetOrderById(string orderId);
        
        /// <summary>
        /// Get all orders for a specific customer
        /// // TODO: Additional filtering on this stage? possible.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        Task<IEnumerable<Order>> GetOrdersByCustomer(string customerId);

        /// <summary>
        /// Creates an order for the customer.
        /// </summary>
        /// <param name="order">Order to create</param>
        /// <returns></returns>
        Task<string> CreateOrderForCustomer(Order order);

    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using AnyCompany.Service.Models;

namespace AnyCompany.Service.Facades
{
    /// <summary>
    /// </summary>
    public interface IOrdersFacade 
    {
        Task<Order> GetOrderById(string orderId);
        Task<IEnumerable<Order>> GetOrdersByCustomer(string customerId);

        /// <summary>
        /// </summary>
        /// <param name="order">Order to create</param>
        /// <returns></returns>
        Task<string> CreateOrderForCustomer(Order order);

    }
}
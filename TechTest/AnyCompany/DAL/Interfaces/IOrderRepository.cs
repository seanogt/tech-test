using AnyCompany.Models;
using System.Collections.Generic;

namespace AnyCompany.DAL
{
    public interface IOrderRepository
    {
        bool Save(Order order);
        IEnumerable<Order> GetOrdersByCustomerId(int customerId);
    }
}

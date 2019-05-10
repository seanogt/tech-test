using System;
using System.Collections.Generic;

namespace AnyCompany.Repositories
{
    public interface IOrderRepository
    {
        bool Save(Order order);
        IEnumerable<Order> GetOrders();
        IEnumerable<Order> GetOrdersByCustomerId(int id);
    }
}

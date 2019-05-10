using System;
using System.Collections.Generic;

namespace AnyCompany.Repositories
{
    public interface IOrderRepository
    {
        void Save(Order order);
        IEnumerable<Order> GetOrders();
        IEnumerable<Order> GetOrdersByCustomerId(int id);
    }
}

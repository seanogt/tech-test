using System;
using System.Collections.Generic;

namespace AnyCompany.Services
{
    interface IOrderService
    {
        bool PlaceOrder(global::AnyCompany.Order order, int customerId);
        IEnumerable<Order> GetOrders();
        IEnumerable<Order> GetOrdersByCustomerId(int customerId);
    }
}

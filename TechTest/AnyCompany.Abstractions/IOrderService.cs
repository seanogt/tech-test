using System.Collections.Generic;
using AnyCompany.Domain;

namespace AnyCompany.Abstractions
{
    public interface IOrderService
    {
        Order PlaceOrder(Order order, int customerId);

        IEnumerable<Order> GetAlOrders();
    }
}
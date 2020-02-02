using AnyCompany.Model;
using System.Collections.Generic;

namespace AnyCompany
{
    public interface IOrderService
    {
        bool PlaceOrder(Order order, int customerId);

        List<Customer> LoadAllCustomersAndOrders();
    }
}

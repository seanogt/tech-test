using System.Collections.Generic;

namespace AnyCompany
{
    public interface ICustomerOrderService
    {
        bool PlaceOrder(Order order, int customerId);
        List<Order> LoadOrders();
        List<CustomerOrder> LoadAllCustomers();

    }
}
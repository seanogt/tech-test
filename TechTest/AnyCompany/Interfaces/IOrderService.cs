using AnyCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.Interfaces
{
    interface IOrderService
    {
        bool PlaceOrder(Order order, int customerId);
        Customer GetCustomer(int customerId);
        List<Order> GetOrders();
    }
}

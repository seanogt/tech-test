using AnyCompany.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.Tests
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Orderservice should be injected using a dependency injection container(e.g. Unity)
            IOrderService orderService = new OrderService();

            List<Customer> customers = orderService.LoadAllCustomersAndOrders();

            Order order = new Order
            {
                Amount = 22d
            };

            orderService.PlaceOrder(order, 1);
        }
    }
}

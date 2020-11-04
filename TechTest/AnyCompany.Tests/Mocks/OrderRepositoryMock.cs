using AnyCompany.DAL;
using AnyCompany.Models;
using System.Collections.Generic;

namespace AnyCompany.Tests.Mocks
{
    public class OrderRepositoryMock : IOrderRepository
    {
        public IEnumerable<Order> GetOrdersByCustomerId(int customerId)
        {
            var orders = new List<Order>();

            if (customerId == 1)
            {
                orders.Add(new Order { OrderId = 1, Amount = 3, VAT = 0.2d, CustomerId = 1 });
                orders.Add(new Order { OrderId = 2, Amount = 4, VAT = 0.2d, CustomerId = 1 });
                orders.Add(new Order { OrderId = 3, Amount = 5, VAT = 0.2d, CustomerId = 1 });
            }

            return orders;
        }

        public bool Save(Order order)
        {
            return true;
        }
    }
}

using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace AnyCompany
{
    public class OrderRepositoryMock : IOrderRepository
    {
        public List<Order> Orders;

        public OrderRepositoryMock()
        {
            Orders = new List<Order>();
        }

        public void Add(Order order)
        {
            Orders.Add(order);
        }

        public Dictionary<int, List<Order>> LoadOrdersForCustomers(IEnumerable<int> customerIds)
        {
            Dictionary<int, List<Order>> dic = new Dictionary<int, List<Order>>();
            foreach (var customerId in customerIds)
            {
                var orders = Orders.Where(x => x.CustomerId == customerId).ToList();
                dic.Add(customerId, orders);
            }

            return dic;
        }

    }
}

using System.Collections.Generic;

namespace AnyCompany
{
    public interface IOrderRepository
    {
        void Add(Order order);
        Dictionary<int, List<Order>> LoadOrdersForCustomers(IEnumerable<int> customerIds);
    }
}
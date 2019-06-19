using System.Collections.Generic;

namespace AnyCompany
{
    public interface IOrderRepository
    {
        void Save(Order order);
        List<Order> LoadAll();
    }
}
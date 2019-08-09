using System.Collections.Generic;
using AnyCompany.Models;

namespace AnyCompany.Data.Contract.Repositories
{
    public interface IOrderRepository
    {
        void Add(Order order);
        IEnumerable<Order> GetList();
    }
}

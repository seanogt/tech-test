using AnyCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.Repositories.OrderRepository
{
    public interface IOrderRepository<T> where T : class
    {
        IEnumerable<Order> GetOrders();
        IEnumerable<T> GetCustomerOrders(int id);
    }
}

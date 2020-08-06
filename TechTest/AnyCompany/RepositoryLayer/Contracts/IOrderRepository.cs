using AnyCompany.Entities;
using AnyCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.RepositoryLayer.Contracts
{
    public interface IOrderRepository<T> where T : class
    {
        IEnumerable<Order> GetOrders();
        CustomerOrderSet GetCustomerOrders(int customerId);
    }
}

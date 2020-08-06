using AnyCompany.Entities;
using AnyCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.RepositoryLayer
{
    public interface IUnitofWork
    {
        CustomerOrderSet GetCustomerOrderSets(int cutomerId);
        List<CustomerOrderSet> GetCustomersWithOrders();
        Customer GetCustomer(int customerId);
        bool SaveOrder(NewOrder newOrder);
    }
}

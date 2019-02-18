using AnyCompany.Entity;
using System.Collections.Generic;

namespace AnyCompany.Repository
{
    public interface IUnitOfWork
    {
        List<Customer> Customers { get; }

        List<Order> Orders { get; }

        List<VAT> VATs { get; }

        List<CustomerOrder> CustomerOrders { get; }

        void SaveChangesAsync();
    }
}

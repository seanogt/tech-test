using System.Collections.Generic;

namespace AnyCompany.DAL
{
    public interface ICustomerRepository
    {
        Customer Get(int CustomerId);
        IEnumerable<Customer> GetAll(bool includeOrders = true);
    }
}

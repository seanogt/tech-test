using System.Collections.Generic;

namespace AnyCompany
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetAllCustomersWithOrders();
    }
}
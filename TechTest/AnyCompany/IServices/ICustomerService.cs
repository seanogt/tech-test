using System;
using System.Collections.Generic;

namespace AnyCompany.IServices
{
    public interface ICustomerService
    {
        bool SaveCustomer(Customer customer);
        List<Customer> GetCustomersWithOrders();
    }
}

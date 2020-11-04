using AnyCompany.Models;
using System.Collections.Generic;

namespace AnyCompany.DAL
{
    public interface ICustomerRepository
    {
        Customer GetCustomerById(int customerId);
        IEnumerable<Customer> GetAllCustomers();
    }
}

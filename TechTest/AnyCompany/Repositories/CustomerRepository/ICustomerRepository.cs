using AnyCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.Repositories.CustomerRepository
{
    public interface ICustomerRepository<T> where T : class
    {
        IEnumerable<Customer> GetCustomerDetails(int customerid);

        IEnumerable<Customer> GetAllCustomers();

    }
}

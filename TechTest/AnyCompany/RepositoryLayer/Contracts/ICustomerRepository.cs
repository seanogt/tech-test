using AnyCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.RepositoryLayer.Contracts
{
    public interface ICustomerRepository<T> where T : class
    {
        Customer GetCustomer(int customerId);
    }
}

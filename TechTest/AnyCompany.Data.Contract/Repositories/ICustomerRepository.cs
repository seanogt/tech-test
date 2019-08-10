using System.Collections.Generic;
using AnyCompany.Models;

namespace AnyCompany.Data.Contract.Repositories
{
    public interface ICustomerRepository
    {
        Customer Load(int customerId);
        IEnumerable<Customer> GetList();
    }
}

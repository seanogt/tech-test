using System.Collections.Generic;

namespace AnyCompany
{
    public interface ICustomerRepository
    {
        Customer Load(int customerId);
        IEnumerable<Customer> LoadAll();
    }
}
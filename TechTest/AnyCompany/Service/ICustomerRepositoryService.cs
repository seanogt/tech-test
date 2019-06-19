using System.Collections.Generic;

namespace AnyCompany
{
    public interface ICustomerRepositoryService
    {
        Customer LoadCustomer(int customerId);

        List<Customer> LoadAllCustomers();
    }
}
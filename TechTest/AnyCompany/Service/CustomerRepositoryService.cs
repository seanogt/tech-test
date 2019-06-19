using System.Collections.Generic;

namespace AnyCompany
{
    public class CustomerRepositoryService : ICustomerRepositoryService
    {
      
        public Customer LoadCustomer(int customerId)
        {
           return CustomerRepository.Load(customerId);
        }

        public List<Customer> LoadAllCustomers()
        {
          return  CustomerRepository.LoadAll();

        }
    }
}
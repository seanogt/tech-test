using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany
{
    public class CustomerRepositoryProxy : ICustomerRepository
    {

        public CustomerRepositoryProxy(string connectionString)
        {
            CustomerRepository.ConnectionString = connectionString;
        }

        public Customer Load(int customerId)
        {
            return CustomerRepository.Load(customerId);
        }

        public IEnumerable<Customer> LoadAll()
        {
            return CustomerRepository.LoadAll();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany
{
    public class CustomerRepositoryMock : ICustomerRepository
    {
        public List<Customer> Customers;

        public CustomerRepositoryMock()
        {
            Customers = new List<Customer>();
        }

        public Customer Load(int customerId)
        {
            return Customers.SingleOrDefault(x => x.CustomerId == customerId);
        }

        public IEnumerable<Customer> LoadAll()
        {
            return Customers;
        }
    }
}

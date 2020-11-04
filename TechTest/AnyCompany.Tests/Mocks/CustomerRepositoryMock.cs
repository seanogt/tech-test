using AnyCompany.DAL;
using AnyCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.Tests.Mocks
{
    public class CustomerRepositoryMock : ICustomerRepository
    {
        public Customer GetCustomerById(int customerId)
        {
            var customer = new Customer();

            if (customerId == 1)
            { 
                customer = new Customer()
                {
                    CustomerId = 1,
                    Country = new Country()
                    {
                        CountryId = 1,
                        Name = "UK",
                        VATRate = 0.2d
                    },
                    DateOfBirth = new DateTime(1982, 2, 26),
                    Name = "John"
                };
            }

            return customer;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            var customers = new List<Customer>();
            customers.Add(new Customer()
            {
                CustomerId = 1,
                CountryId = 1,
                DateOfBirth = new DateTime(1982, 2, 26),
                Name = "John"
            });

            customers.Add(new Customer()
            {
                CustomerId = 2,
                CountryId = 1,
                DateOfBirth = new DateTime(1983, 2, 26),
                Name = "Jack"
            });

            customers.Add(new Customer()
            {
                CustomerId = 3,
                CountryId = 1,
                DateOfBirth = new DateTime(1984, 2, 26),
                Name = "Jill"
            });

            return customers;
        }
    }
}

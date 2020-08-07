using AnyCompany.DAL.Interfaces;
using AnyCompany.DAL.Models;
using System.Collections.Generic;

namespace AnyCompany.BLL.Services
{
    //Creating DataSource not responsibilty of Customer Service
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Customer RetrieveACustomer(int CustomerId)
        {
            return _customerRepository.Get(CustomerId);
        }

        //Return Customers with linked Orders by default
        public IEnumerable<Customer> RetrieveCustomers(bool includeOrders = true)
        {
            return _customerRepository.GetAll(includeOrders);
        }
    }
}

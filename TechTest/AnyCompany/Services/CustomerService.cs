using System.Collections.Generic;
using System.Linq;

namespace AnyCompany
{
    public class CustomerService : ICustomerService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(IOrderRepository orderRepository, ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
        }

        public IEnumerable<Customer> GetAllCustomersWithOrders()
        {
            var customers = _customerRepository.LoadAll();
            if (customers == null)
            {
                return new List<Customer>();
            }

            var ordersDictionary = _orderRepository.LoadOrdersForCustomers(customers.Select(x => x.CustomerId));

            foreach (var customer in customers)
            {
                if (ordersDictionary.ContainsKey(customer.CustomerId))
                {
                    customer.Orders = ordersDictionary[customer.CustomerId];
                }
            }

            return customers;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany
{
    public class CustomerService
    {
        private OrderRepository orderRepo;

        public CustomerService()
        {
            orderRepo = new OrderRepository();
        }

        public List<CustomerOrders> GetAllCustomersOrders()
        {
            var customerlst = CustomerRepository.GetAllCustomers();
            var customerOrders = new List<CustomerOrders>();

            foreach (Customer cus in customerlst)
            {
                CustomerOrders cusord = new CustomerOrders()
                {
                    customer = cus,
                    orders = orderRepo.GetAllCustomerOrders(cus.CustomerId),
                };

                customerOrders.Add(cusord);

            }
            return customerOrders;

        }
    }
}

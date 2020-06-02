using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnyCompany.DataRepositories;
using AnyCompany.Models;

namespace AnyCompany.Services
{
    public class CustomerService
    {

        private OrderRepository _orderRepository;

        public CustomerService()
        {
            _orderRepository = new OrderRepository();
        }

        /// <summary>
        /// Gets a list of all the customers with a list of all their orders, respectively
        /// </summary>
        /// <returns>List of customers with orders</returns>
        public List<CustomerOrderModel> GetAllOrdersForAllCustomers()
        {
            try
            {
                List<CustomerOrderModel> returnList = new List<CustomerOrderModel>();

                var allCustomers = CustomerRepository.GetAllCustomers();
                var allOrders = _orderRepository.GetAllOrders(); //getting all the orders like this is not ideal. Ideally we want to have the database run the query for us. 
                // an alternative is to get the record for each customer, but then we create connections for each user. So this is a performance trade-off
                // it was decided not to write a query at this time as it is out of scope. Rather a proper ORM is suggested which can handle this for us.

                foreach (var customer in allCustomers)
                {
                   returnList.Add(new CustomerOrderModel
                   {
                       Customer = customer,
                       Orders = allOrders.Where(x => x.CustomerId == customer.CustomerId).ToList()
                   });
                }

                return returnList;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Customer service GetAll error: {ex.Message}");
                return null;
            }            

        }
    }
}

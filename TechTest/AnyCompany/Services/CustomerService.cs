using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AnyCompany
{
    public class CustomerService
    {
        public List<Customer> GetCustomersWithOrders() {
            var customers = new List<Customer>();
            try
            {
                customers = CustomerRepository.GetCustomers();
                foreach (var customer in customers)
                {
                    customer.Orders = OrderRepository.GetOrders(customer.CustomerId);
                }
            }
            catch (SqlException ex)
            {
                //{code to log error}

                return customers;
            }
            catch (Exception ex)
            {
                //{code to log error}

                return customers;
            }

            return customers;
        }
    }
}

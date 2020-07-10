using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using AnyCompany.IServices;

namespace AnyCompany
{
    public class CustomerService : ICustomerService
    {
        public bool SaveCustomer(Customer customer)
        {
            int result = 0;
            try
            {
                 result = CustomerRepository.Save(customer);
            }
            catch (SqlException ex)
            {
                //{code to log error}
            }
            catch (Exception ex)
            {
                //{code to log error}
            }
            if (result == 0) { return false; }
            return true;
        }

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

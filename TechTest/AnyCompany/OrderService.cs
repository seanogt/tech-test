using AnyCompany.Models;
using AnyCompany.Repository;
using System;
using System.Collections.Generic;

namespace AnyCompany
{
    public class OrderService
    {
        public bool PlaceOrder(Order order)
        {
            try
            {
                Customer customer = CustomerRepository.Load(order.CustomerId);

                if (customer != null)
                {
                    order.CustomerId = customer.CustomerId;

                    if (order.Amount == 0)
                        return false;

                    if (customer.Country == "UK")
                        order.VAT = 0.2d;
                    else
                        order.VAT = 0;

                    order = OrderRepository.Save(order);

                    return order?.OrderId > 0;
                }
                else
                {
                    //To-do: Implement proper error logging
                    Console.WriteLine("Error : Customer doesn't exist");
                    return false;
                }
            }
            catch (Exception ex)
            {
                //To-do: Implement proper error logging
                Console.WriteLine("Error : " + ex.Message);
                return false;
            }
        }

        public List<Customer> RetrieveCustomerOrders()
        {
            try
            {
                List<Customer> customers = CustomerRepository.LoadCollection();

                foreach (var customer in customers)
                {
                    customer.Orders = OrderRepository.LoadCollection(customer.CustomerId);
                }

                return customers;
            }
            catch (Exception ex)
            {
                //To-do: Implement proper error logging
                Console.WriteLine("Error : " + ex.Message);
                return new List<Customer>();
            }
        }

        public Customer NewCustomer(Customer customer)
        {
            try
            {
                return CustomerRepository.Save(customer);
            }
            catch (Exception ex)
            {
                //To-do: Implement proper error logging
                Console.WriteLine("Error : " + ex.Message);
                return customer;
            }
        }
    }
}

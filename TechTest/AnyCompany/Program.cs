using AnyCompany.BUL.Helpers;
using AnyCompany.BUL.Services;
using AnyCompany.DAL;
using AnyCompany.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnyCompany
{
    class Program
    {        static void Main(string[] args)
        {
            CustomerService c = new CustomerService(new CustomerRepositoryWrapper());
            Customer customer = c.RetrieveACustomer(1);
            if (customer != null)
            {
                Console.WriteLine("Customer is " + customer.Name);
                Console.WriteLine();

                IEnumerable<Customer> customers = c.RetrieveCustomers(true);
                Console.WriteLine("Customers are:");
                customers.ToList().ForEach(i => Console.Write("Name: {0}\t Order counts: {1}\n", i.Name, i.Orders.Count));

                Console.WriteLine("\n\n=============================");
                OrderService s = new OrderService(new OrderRepositoryWrapper(), new CustomerRepositoryWrapper());
                Order order = new Order { Amount = 2456, CustomerId = customer.CustomerId, VAT = BusinessRulesHelper.DetermineVat(customer.Country) };
                bool result = s.PlaceOrder(order, customer.CustomerId);
                Console.WriteLine("Added Order succesfully = " + result);
            }
            else
            {
                Console.WriteLine("Could be no data in the DB... Mmm..");
            }
            Console.ReadKey();
        }
    }
}

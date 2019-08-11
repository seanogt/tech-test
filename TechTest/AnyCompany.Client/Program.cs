﻿using System;
using System.Linq;
using AnyCompany.Abstractions;
using AnyCompany.Domain;
using AnyCompany.Services;
using AnyCompany.Services.Repositories;
using StructureMap;

namespace AnyCompany.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = Container.For<ClientRegistry>();

            //below Save customer is not required, for testing purpose
            var customer = new Customer
            {
                Country = "UK",
                DateOfBirth = DateTime.Now.AddYears(-20),
                Name = "tester2"
            };
            var orderRepository = container.GetInstance<IOrderRepository>();
            orderRepository.Save(customer);

            var order = new Order
            {
                Amount = 120.0
            };
            Console.WriteLine("Placing an order for customer id 1. ");
            var orderService = container.GetInstance<OrderService>();
            orderService.PlaceOrder(order, 1);

            Console.WriteLine("Getting all customers and their linked orders. ");
            var orders = orderService.GetAlOrders().ToList();
            var customers = orderService.GetAlCustomers().ToList();

            foreach (var item in customers)
            {
                if (item.Orders == null || !item.Orders.Any())
                {
                    continue;
                }
                Console.WriteLine($"customer id: {item.CustomerId}, name: {item.Name}, total orders: {item.Orders.Count} ");

                foreach (var linkedOrder in item.Orders)
                {
                    Console.WriteLine($"order id: {linkedOrder.OrderId}, amount: {linkedOrder.Amount}, VAT: {linkedOrder.VAT} ");
                }
            }

            Console.WriteLine("Listing all orders and the linked customer. ");
            foreach (var item in orders)
            {
                if (item.Customer == null)
                {
                    continue;
                }
                Console.WriteLine($"order id: {item.OrderId}, amount: {item.Amount}, VAT: {item.VAT}, customer id: {item.Customer.CustomerId}, customer name: {item.Customer.Name} ");
            }
            Console.ReadLine();
        }
    }

    public class ClientRegistry : Registry
    {
        public ClientRegistry()
        {
            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.WithDefaultConventions();
            });
            For<IOrderRepository>().Use<OrderRepository>();
        }
    }
}

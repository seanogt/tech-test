using System;
using System.Collections.Generic;
using AnyCompany.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AnyCompany.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private AnyCompany.Interfaces.IOrderService _OrderService;

        [TestInitialize]
        public void TestSetup()
        {
            _OrderService = new OrderService();
        }

        [TestMethod]
        public void TestPlaceOrderForASingleCustomerSuccess()
        {
            var order = Getorder();
            var customer = Getcustomer();

            var IsSuccess = _OrderService.PlaceOrder(order, customer.CustomerId);

            Assert.IsTrue(IsSuccess);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "An error has occured placing the order.")]
        public void TestPlaceOrderForASingleCustomerFail()
        {
            var order = Getorder();
            order.Amount = 0;

            var customer = Getcustomer();

            var IsSuccess = _OrderService.PlaceOrder(order, customer.CustomerId);

            Assert.IsTrue(IsSuccess);
        }

        [TestMethod]
        public void TestGetAllCustomersAndTheirOrdersSuccess()
        {
            var cOrders = _OrderService.GetOrders();

            Assert.AreNotEqual(0, cOrders.Count);

            foreach (var custOrder in cOrders)
            {
                Console.WriteLine($"Name: {custOrder.Customer.Name} -Country: {custOrder.Customer.Country} - Amount: ${custOrder.Amount} VAT: {custOrder.VAT} ");
            }
        }

        public List<Customer> Customers()
        {
            List<Customer> ListOfCustomers = new List<Customer>()
            {
                new Customer()
                {
                   CustomerId = 1,  Name = "Kgaugelo", Country = "UK", DateOfBirth = new DateTime(1989, 07, 18)
                }, new Customer()
                {
                    CustomerId = 2, Name = "Maidi", Country = "SA", DateOfBirth = new DateTime(1993, 02, 23)
                }
            };

            return ListOfCustomers;
        }

        public Customer Getcustomer()
        {
            Customer steve = new Customer { CustomerId = 1, Name = "Steve", Country = "UK", DateOfBirth = new DateTime(2001, 2, 13) };

            return steve;
        }

        public List<Order> OrderList()
        {
            Customer customer = new Customer();
            customer = Getcustomer();

            Order den = Getorder();
            var ordersList = new List<Order>()
            {
                new Order { OrderId = 1, Amount = 1229.76, CustomerId = 1, VAT = 0, Customer = Customers()[0] },
                new Order { OrderId = 2, Amount = 432.21, CustomerId = 1, VAT = 0, Customer = Customers()[1] },
                new Order { OrderId = 3, Amount = 139.50, CustomerId = 2, VAT = 0, Customer =  customer }, den
            };

            return ordersList;
        }

        public Order Getorder()
        {
            Order den = new Order();
            Customer customer = new Customer();
            den.Amount = 139.50;
            den.CustomerId = 2;
            den.VAT = 0;
            customer.CustomerId = 2;
            customer.Name = "Den";
            customer.Country = "SA";
            customer.DateOfBirth = new DateTime(2001, 2, 13);
            den.Customer = customer;

            return den;
        }
    }
}

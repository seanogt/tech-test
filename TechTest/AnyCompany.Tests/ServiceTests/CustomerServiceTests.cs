using System;
using System.Collections.Generic;
using System.Linq;
using AnyCompany;
using AnyCompany.BUL.Helpers;
using AnyCompany.BUL.Services;
using AnyCompany.DAL;
using AnyCompany.DAL.Repositories;
using Moq;
using NUnit.Framework;

namespace InvestecUnitTests
{
    [TestFixture]
    public class CustomerServiceTests 
    {
        private Mock<ICustomerRepository> _customerService = new Mock<ICustomerRepository>();
        private CustomerService customerService;
        private readonly List<Customer> customers = new List<Customer>
            {
                new Customer
                {
                    Name = "Mike America", Country = "US", DateOfBirth = DateTime.Parse("1987-06-01"), CustomerId = 1
                },
                       new Customer
                {
                    Name = "Jonathan Canada", Country = "CN", DateOfBirth = DateTime.Parse("1925-06-01"), CustomerId = 2
                },
                              new Customer
                {
                    Name = "FROM THE UK CHINKU", Country = "UK", DateOfBirth = DateTime.Parse("2001-06-01"), CustomerId = 3
                }
                              ,
                              new Customer
                {
                    Name = "Not in Database Adams", Country = "AG", DateOfBirth = DateTime.Parse("1982-12-12"), CustomerId = 50
                }
            };
        private readonly List<Customer> customersWithOrders = new List<Customer>
            {
                new Customer
                {
                    Name = "I Have Order too", Country = "NM", DateOfBirth = DateTime.Parse("1987-06-01"), CustomerId = 12
                    , Orders = new List<Order>()
                    {
                        new Order
                        {
                            Amount = 500,
                            OrderId = 12,
                            CustomerId = 1,
                            VAT = 15
                        }
                    }
                },
                       new Customer
                {
                    Name = "I Have Order", Country = "ZW", DateOfBirth = DateTime.Parse("1925-06-01"), CustomerId = 13
                           ,            Orders = new List<Order>()
                    {
                        new Order
                        {
                            Amount = 500,
                            OrderId = 12,
                            CustomerId = 1,
                            VAT = 15
                        }
                    }
                }
            };

        [TestCase]
        public void GIVEN_EXISTINGId_RETURNS_MATCHING_CUSTOMER()
        {
            //Arrange
            Customer matchingCustomer = customers[2];
            _customerService.Setup(x => x.Get(matchingCustomer.CustomerId)).Returns(matchingCustomer);

            //Act
            customerService = new CustomerService(_customerService.Object);
            Customer returnedCustomer = customerService.RetrieveACustomer(matchingCustomer.CustomerId);

            //Asset
            Assert.AreSame(returnedCustomer, matchingCustomer);
            Assert.AreEqual(returnedCustomer.CustomerId, matchingCustomer.CustomerId);
        }

        [TestCase]
        public void GIVEN_WRONGId_RETURNS_NONMATCHING_CUSTOMER()
        {
            //Arrange
            Customer customerToGet = customers[2];
            Customer notMatchingCustomer = customers[0];
            _customerService.Setup(x => x.Get(customerToGet.CustomerId)).Returns(notMatchingCustomer);

            //Act
            customerService = new CustomerService(_customerService.Object);
            Customer returnedCustomer = customerService.RetrieveACustomer(customerToGet.CustomerId);

            //Asset
            Assert.AreNotSame(returnedCustomer, customerToGet);
        }

        [TestCase]
        public void GIVEN_NONEXISTENTId_RETURNS_EMPTYCUSTOMEROBJECT()
        {
            //Arrange
            int NonExtentId = 344;

            _customerService.Setup(x => x.Get(NonExtentId)).Returns(new Customer());

            //Act
            customerService = new CustomerService(_customerService.Object);
            Customer returnedCustomer = customerService.RetrieveACustomer(NonExtentId);

            //Asset
            Assert.True(returnedCustomer.CustomerId == 0);
        }

        [TestCase]
        public void GIVEN_TRUE_ARGUMENT_RETURNS_CUSTOMERS_WITH_LINKED_ORDERS()
        {
            //Arrange
            _customerService.Setup(x => x.GetAll(true)).Returns(customersWithOrders);

            //Act
            customerService = new CustomerService(_customerService.Object);
            IEnumerable<Customer> returnedCustomers = customerService.RetrieveCustomers(true);
            int orderCount = 0;
            returnedCustomers.ToList().ForEach(i => orderCount += i.Orders.Count);

            //Asset
            Assert.AreSame(customersWithOrders, returnedCustomers);
            Assert.True(orderCount > 0);
        }

        [TestCase]
        public void GIVEN_FALSE_ARGUMENT_RETURNS_CUSTOMERS_WITHOUT_LINKED_ORDERS()
        {
            //Arrange
            _customerService.Setup(x => x.GetAll(false)).Returns(customers);

            //Act
            customerService = new CustomerService(_customerService.Object);
            IEnumerable<Customer> returnedCustomers = customerService.RetrieveCustomers(false);

            int orderCount = 0;
            returnedCustomers.ToList().ForEach(customer => orderCount += customer.Orders.Count);

            //Asset
            Assert.AreEqual(orderCount, 0);
            Assert.AreNotSame(returnedCustomers, customersWithOrders);
        }
    }
}

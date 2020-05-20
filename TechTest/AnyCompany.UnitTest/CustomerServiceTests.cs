using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AnyCompany.UnitTest
{
    [TestClass]
    public class CustomerServiceTests
    {
        private OrderRepositoryMock _orderRepository;
        private CustomerRepositoryMock _customerRepository;
        private ICustomerService _customerService;

        public CustomerServiceTests()
        {
            var customerId1 = 1;
            var customerId2 = 2;

            _customerRepository = new CustomerRepositoryMock
            {
                Customers = new List<Customer>()
                {
                    new Customer()
                    {
                        CustomerId = customerId1,
                        Country = "ZA",
                        Name = "bob",
                        DateOfBirth = new DateTime(1980, 01, 06)
                    },
                    new Customer()
                    {
                        CustomerId = customerId2,
                        Country = "uk",
                        Name = "jill",
                        DateOfBirth = new DateTime(1980, 01, 06)
                    }
                }
            };

            _orderRepository = new OrderRepositoryMock
            {
                Orders = new List<Order>()
                {
                    new Order()
                    {
                        CustomerId = customerId1,
                        OrderId = 1,
                        Amount = 110,
                        VAT = 0
                    },
                    new Order()
                    {
                        CustomerId = customerId1,
                        OrderId = 2,
                        Amount = 100,
                        VAT = 0
                    },
                    new Order()
                    {
                        CustomerId = customerId1,
                        OrderId = 3,
                        Amount = 90,
                        VAT = 0.2
                    },
                    new Order()
                    {
                        CustomerId = customerId1,
                        OrderId = 4,
                        Amount = 80,
                        VAT = 0.2
                    },
                }
            };

            _customerService = new CustomerService(_orderRepository, _customerRepository);
        }

        [TestMethod]
        public void GetAllCustomersWithOrders_Successfully()
        {
            // Arrange
           
            // Act
            var customers = _customerService.GetAllCustomersWithOrders();

            // Assert
            Assert.AreEqual(_customerRepository.Customers.Count, customers.Count());
            Assert.AreEqual(_orderRepository.Orders.Count, customers.Sum(x => x.Orders.Count()));
        }

        [TestMethod]
        public void When_NoCustomersExist_Return_EmptyList()
        {
            // Arrange
            _customerRepository.Customers = new List<Customer>();

            // Act
            var customers = _customerService.GetAllCustomersWithOrders();

            // Assert
            Assert.AreEqual(0, customers.Count());
        }

        [TestMethod]
        public void When_NoOrdersExist_Return_CustomersWith_EmptyOrdersList()
        {
            // Arrange
            _orderRepository.Orders = new List<Order>();

            // Act
            var customers = _customerService.GetAllCustomersWithOrders();

            // Assert
            Assert.AreEqual(_customerRepository.Customers.Count, customers.Count());
            Assert.AreEqual(0, customers.Sum(x => x.Orders.Count()));
        }

    }
}

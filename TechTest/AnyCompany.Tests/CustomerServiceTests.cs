// <copyright file="CustomerServiceTests.cs" company="Investec Bank">
// Copyright © Investec Bank 2018
// </copyright>

namespace AnyCompany.Tests
{
    using AnyCompany.Service;
    using AnyCompany.Tests.MockRepository;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;

    /// <summary>
    /// Provides unit tests for the <see cref="CustomerService"/> class.
    /// </summary>
    [TestClass]
    public class CustomerServiceTests
    {
        /// <summary>
        /// Load all customers with orders
        /// Some customers exist without orders
        /// Get back the customers, no orders.
        /// </summary>
        [TestMethod]
        public void LoadAllCustomerWithOrders_ExistingCustomersNoOrders_GetCustomersNoOrders()
        {
            // Arrange
            var customerRepository = new MockCustomerRepository();
            var orderRepository = new MockOrderRepository();
            var customerService = new CustomerService(customerRepository, orderRepository);

            // Act
            var customersWithOrders = customerService.LoadAllCustomersWithOrders();

            // Assert
            Assert.IsTrue(customersWithOrders.Count() == 2);
            foreach (var (customer, orders) in customersWithOrders)
            {
                Assert.IsTrue(orders.Count() == 0);
            }
        }

        /// <summary>
        /// Load all customers with orders
        /// Some customers exist with orders
        /// Get back the customers, with correct number of orders.
        /// </summary>
        [TestMethod]
        public void LoadAllCustomerWithOrders_ExistingCustomersWithOrders_GetCustomersCorrectOrderNumbers()
        {
            // Arrange
            var customerRepository = new MockCustomerRepository();
            var orderRepository = new MockOrderRepository();
            var customerService = new CustomerService(customerRepository, orderRepository);
            var allCustomers = customerRepository.LoadAll().ToList();

            orderRepository.Save(new Order()
            {
                OrderId = default,
                Amount = 10,
                VAT = default,
                CustomerId = allCustomers[0].CustomerId,
            });

            orderRepository.Save(new Order()
            {
                OrderId = default,
                Amount = 20,
                VAT = default,
                CustomerId = allCustomers[0].CustomerId,
            });

            orderRepository.Save(new Order()
            {
                OrderId = default,
                Amount = 30,
                VAT = default,
                CustomerId = allCustomers[1].CustomerId,
            });

            // Act
            var customersWithOrders = customerService.LoadAllCustomersWithOrders();

            // Assert
            Assert.IsTrue(customersWithOrders.Count() == 2);
            foreach (var (customer, orders) in customersWithOrders)
            {
                if (customer.CustomerId == allCustomers[0].CustomerId)
                {
                    Assert.IsTrue(orders.Count() == 2);
                }
                else if (customer.CustomerId == allCustomers[1].CustomerId)
                {
                    Assert.IsTrue(orders.Count() == 1);
                }
            }
        }
    }
}

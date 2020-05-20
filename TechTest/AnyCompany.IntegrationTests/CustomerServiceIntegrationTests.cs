using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AnyCompany.IntegrationTests
{
    [TestClass]
    public class CustomerServiceIntegrationTests
    {
        private ICustomerService _customerService;

        public CustomerServiceIntegrationTests()
        {
            Config config = new Config();

            var customerRepository = new CustomerRepositoryProxy(config.CustomerDbConnectionString);

            var orderRepository = new OrderRepository(config.OrdersDbConnectionString);

            _customerService = new CustomerService(orderRepository, customerRepository);
        }

        [TestMethod]
        public void GetAllCustomersWithOrders_Successfully()
        {
            // Arrange
           
            // Act
            var customers = _customerService.GetAllCustomersWithOrders();

            // Assert
            Assert.IsTrue(customers.Count() > 0);
            foreach (var customer in customers)
            {
                Assert.IsTrue(customer.Orders.Count() > 0);
            }
        }

    }
}

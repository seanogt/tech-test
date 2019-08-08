// <copyright file="OrderServiceTests.cs" company="Investec Bank">
// Copyright © Investec Bank 2018
// </copyright>

namespace AnyCompany.Tests
{
    using AnyCompany.Service;
    using AnyCompany.Tests.MockRepository;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;

    /// <summary>
    /// Provides unit tests for the <see cref="OrderService"/> class.
    /// </summary>
    [TestClass]
    public class OrderServiceTests
    {
        /// <summary>
        /// Save
        /// An order linked to a UK customer
        /// VAT is 0.2d.
        /// </summary>
        [TestMethod]
        public void PlaceOrder_LinkedToUkCustomer_VatIsTwentyPercent()
        {
            // Arrange
            var orderRepository = new MockOrderRepository();
            var customerRepository = new MockCustomerRepository();
            var orderService = new OrderService(orderRepository, customerRepository);
            var ukCustomer = customerRepository.LoadAll().First();

            // Act
            var order = new Order()
            {
                OrderId = default,
                Amount = 20,
                VAT = default,
                CustomerId = ukCustomer.CustomerId,
            };

            orderService.PlaceOrder(order);

            // Assert
            Assert.IsTrue(ukCustomer.Country == "UK");
            Assert.IsTrue(order.VAT == 0.2d);
        }

        /// <summary>
        /// Save
        /// An order linked to a non-UK customer
        /// VAT is 0.
        /// </summary>
        [TestMethod]
        public void PlaceOrder_LinkedToNonUkCustomer_VatIsZero()
        {
            // Arrange
            var orderRepository = new MockOrderRepository();
            var customerRepository = new MockCustomerRepository();
            var orderService = new OrderService(orderRepository, customerRepository);
            var nonUkCustomer = customerRepository.LoadAll().Skip(1).First();

            // Act
            var order = new Order()
            {
                OrderId = default,
                Amount = 20,
                VAT = default,
                CustomerId = nonUkCustomer.CustomerId,
            };

            orderService.PlaceOrder(order);

            // Assert
            Assert.IsTrue(nonUkCustomer.Country != "UK");
            Assert.IsTrue(order.VAT == 0d);
        }
    }
}

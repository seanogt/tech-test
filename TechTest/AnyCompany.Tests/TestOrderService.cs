using AnyCompany.Models;
using AnyCompany.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.Tests
{
    [TestClass]
    public class TestOrderService
    {
        private readonly OrderService _orderService;

        public TestOrderService()
        {
            _orderService = new OrderService(
                new OrderRepositoryMock());
        }

        [TestMethod]
        public void PlaceOrder_OrderCreated()
        {
            //----Arrange
            var customer = new Customer
            {
                CustomerId = 1,
                Country = new Country()
                {
                    CountryId = 1,
                    Name = "UK",
                    VATRate = 0.2d
                },
                Name = "John"
            };

            var order = new Order(10, customer);

            ////----Act
            var response = _orderService.PlaceOrder(order);

            ////----Assert
            Assert.IsTrue(response);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PlaceOrder_ZeroAmount_ThrowsException()
        {
            //----Arrange
            var customer = new Customer
            {
                CustomerId = 1,
                Country = new Country()
                {
                    CountryId = 1,
                    Name = "UK",
                    VATRate = 0.2d
                },
                Name = "John"
            };

            var order = new Order(0, customer);

            ////----Act
            var response = _orderService.PlaceOrder(order);
        }

        [TestMethod]
        public void GetOrdersByCustomerId_ValidCustomerId_ReturnsCustomerOrders()
        {
            var response = _orderService.GetOrdersByCustomerId(1);

            ////----Assert
            Assert.IsNotNull(response);
            Assert.IsTrue(response is List<Order>);
            Assert.IsTrue(response.Count() > 0);
        }

        [TestMethod]
        public void GetOrdersByCustomerId_InvalidCustomerId_ReturnsEmptyList()
        {
            var response = _orderService.GetOrdersByCustomerId(5);

            Assert.IsNotNull(response);
            Assert.IsTrue(response is List<Order>);
            Assert.IsTrue(response.Count() <= 0);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AnyCompany.UnitTest
{
    [TestClass]
    public class OrderServiceTests
    {
        private OrderRepositoryMock _orderRepository;
        private CustomerRepositoryMock _customerRepository;
        private IOrderService _orderService;

        public OrderServiceTests()
        {
            _orderRepository = new OrderRepositoryMock();
            _customerRepository = new CustomerRepositoryMock();
            _orderService = new OrderService(_orderRepository, _customerRepository);
        }

        [TestMethod]
        public void PlaceOrder_Successfully()
        {
            // Arrange
            Customer customer = new Customer()
            {
                CustomerId = 1,
                Country = "ZA",
                Name = "bob",
                DateOfBirth = new DateTime(1980, 01, 06)
            };
            _customerRepository.Customers = new List<Customer>()
            {
                 customer
            };

            Order newOrder = new Order()
            {
                CustomerId = customer.CustomerId,
                OrderId = 1,
                Amount = 100,
                VAT = 100
            };

            // Act
            var isOrderPlaced = _orderService.PlaceOrder(newOrder, customer.CustomerId);

            // Assert
            Assert.AreEqual(true, isOrderPlaced);

            var savedOrder = _orderRepository.Orders.SingleOrDefault(x => x.CustomerId == newOrder.CustomerId && x.OrderId == newOrder.OrderId);
            Assert.AreEqual(newOrder.Amount, savedOrder.Amount);
            Assert.AreEqual(0, savedOrder.VAT);
        }

        [TestMethod]
        public void PlaceOrder_Successfully_UkCustomer()
        {
            // Arrange
            Customer customer = new Customer()
            {
                CustomerId = 1,
                Country = "UK",
                Name = "bob",
                DateOfBirth = new DateTime(1980, 01, 06)
            };
            _customerRepository.Customers = new List<Customer>()
            {
                 customer
            };

            Order newOrder = new Order()
            {
                CustomerId = customer.CustomerId,
                OrderId = 1,
                Amount = 100,
                VAT = 100
            };

            // Act
            var isOrderPlaced = _orderService.PlaceOrder(newOrder, customer.CustomerId);

            // Assert
            Assert.AreEqual(true, isOrderPlaced);

            var savedOrder = _orderRepository.Orders.SingleOrDefault(x => x.CustomerId == newOrder.CustomerId && x.OrderId == newOrder.OrderId);
            Assert.AreEqual(newOrder.Amount, savedOrder.Amount);
            Assert.AreEqual(0.2d, savedOrder.VAT);
        }

        [TestMethod]
        public void When_OrderAmount_Is0_Return_False()
        {
            // Arrange
            Customer customer = new Customer()
            {
                CustomerId = 1,
                Country = "UK",
                Name = "bob",
                DateOfBirth = new DateTime(1980, 01, 06)
            };
            _customerRepository.Customers = new List<Customer>()
            {
                 customer
            };

            Order newOrder = new Order()
            {
                CustomerId = customer.CustomerId,
                OrderId = 1,
                Amount = 0,
                VAT = 100
            };

            // Act
            var isOrderPlaced = _orderService.PlaceOrder(newOrder, customer.CustomerId);

            // Assert
            Assert.AreEqual(false, isOrderPlaced);

            var savedOrder = _orderRepository.Orders.SingleOrDefault(x => x.CustomerId == newOrder.CustomerId && x.OrderId == newOrder.OrderId);
            Assert.IsNull(savedOrder);
        }

        [TestMethod]
        public void When_Order_IsNull_Return_False()
        {
            // Arrange
            Customer customer = new Customer()
            {
                CustomerId = 1,
                Country = "UK",
                Name = "bob",
                DateOfBirth = new DateTime(1980, 01, 06)
            };
            _customerRepository.Customers = new List<Customer>()
            {
                 customer
            };

            // Act
            var isOrderPlaced = _orderService.PlaceOrder(null, customer.CustomerId);

            // Assert
            Assert.AreEqual(false, isOrderPlaced);
        }

        [TestMethod]
        public void When_Customer_NotFound_Return_False()
        {
            // Arrange
            _customerRepository.Customers = new List<Customer>();

            Order newOrder = new Order()
            {
                CustomerId = 1,
                OrderId = 1,
                Amount = 100,
                VAT = 100
            };

            // Act
            var isOrderPlaced = _orderService.PlaceOrder(newOrder, newOrder.CustomerId);

            // Assert
            Assert.AreEqual(false, isOrderPlaced);

            var savedOrder = _orderRepository.Orders.SingleOrDefault(x => x.CustomerId == newOrder.CustomerId && x.OrderId == newOrder.OrderId);
            Assert.IsNull(savedOrder);
        }
    }
}

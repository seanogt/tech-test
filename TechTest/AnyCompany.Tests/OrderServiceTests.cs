using System;
using AnyCompany.Data.Contract.Repositories;
using AnyCompany.Models;
using AnyCompany.Services;
using AnyCompany.Services.Dtos;
using AnyCompany.Services.Exceptions;
using AnyCompany.Services.Services;
using Moq;
using NUnit.Framework;

namespace AnyCompany.Tests
{
    [TestFixture]
    public class OrderServiceTests
    {
        private Mock<IOrderRepository> _orderRepositoryMock;
        private Mock<ICustomerRepository> _customerRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _customerRepositoryMock = new Mock<ICustomerRepository>();
        }

        [TestCase("UK", 0.2)]
        [TestCase("FR", 0)]
        public void PlaceOrder_ShouldLoadACustomerAndPlaceAnOrder_UK(string country, double vat)
        {
            // Arrange.
            const int CustomerId = 23;
            var orderDto = GetOrderDto();

            var customer = GetCustomer(country);
            _customerRepositoryMock.Setup(r => r.Load(CustomerId))
                .Returns(customer);

            Order inOrder = null;
            _orderRepositoryMock.Setup(o => o.Add(It.IsAny<Order>()))
                .Callback<Order>(o => inOrder = o);

            var orderService = GetOrderService();

            // Act.
            var result = orderService.PlaceOrder(orderDto, CustomerId);

            // Assert.
            Assert.IsTrue(result);
            Assert.AreEqual(orderDto.Amount, inOrder.Amount);
            Assert.AreEqual(vat, inOrder.VAT);
            Assert.AreEqual(CustomerId, inOrder.CustomerId);
        }

        [Test]
        public void PlaceOrder_CustomerNotFound_ShouldThrowException()
        {
            // Arrange.
            const int CustomerId = 44;

            _customerRepositoryMock.Setup(r => r.Load(CustomerId))
                .Returns((Customer)null);

            var orderService = GetOrderService();

            // Act/Assert.
            Assert.Throws<CustomerNotFoundException>(() => orderService.PlaceOrder(GetOrderDto(), CustomerId));
        }

        [Test]
        public void PlaceOrder_AmountIs0_ShouldReturnFalse()
        {
            // Arrange.
            const int CustomerId = 23;
            var orderDto = GetOrderDto();

            var customer = GetCustomer("UK");
            _customerRepositoryMock.Setup(r => r.Load(CustomerId))
                .Returns(customer);

            orderDto.Amount = 0;

            var orderService = GetOrderService();

            // Act.
            var result = orderService.PlaceOrder(orderDto, CustomerId);

            // Assert.
            Assert.IsFalse(result);
        }

        private Customer GetCustomer(string country)
        {
            return new Customer
            {
                Name = "John Smith",
                DateOfBirth = DateTime.UtcNow.AddYears(-31),
                Country = country
            };
        }

        private OrderService GetOrderService()
        {
            return new OrderService(
                _orderRepositoryMock.Object,
                _customerRepositoryMock.Object);
        }

        private OrderDto GetOrderDto()
        {
            return new OrderDto
            {
                Amount = 20,
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AnyCompany.Abstractions;
using AnyCompany.Domain;
using AnyCompany.Repository;
using AnyCompany.Services;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AnyCompany.UnitTests
{
    [TestClass]
    public class OrderServiceShould
    {
        private Order _order1;
        private Order _order2;
        private Customer _customer;
        private Mock<IOrderRepository> _mockOrderRepository;
        private List<Order> _orders;
        private Mock<DbSet<Order>> _mockOrderDbSet;
        private Mock<OrdersDbContext> _mockOrderDbContext;

        public OrderServiceShould()
        {
            AssumeOrderIsInitialised();
            AssumeDbContextIsInitialised();
            AssumDbContextIsSetup();
            AssumeRepositoryIsInitialised();
            AssumRepositoryIsSetup();
        }

        [TestMethod]
        public void ShouldPlaceOrderOk()
        {
            var service = new OrderService(_mockOrderRepository.Object);
            var order = service.PlaceOrder(_order1, 1);

            _mockOrderRepository.Verify(m => m.Save(It.IsAny<Order>()), Times.Once());
            order.Customer.CustomerId.Should().Be(1);
        }

        [TestMethod]
        public void ShouldPlaceOrderSetCorrectVAT()
        {
            var service = new OrderService(_mockOrderRepository.Object);
            var order = service.PlaceOrder(_order1, 1);

            _mockOrderRepository.Verify(m => m.Save(It.IsAny<Order>()), Times.Once());
            order.Customer.CustomerId.Should().Be(1);
            order.VAT.Should().Be(0.2d);
        }

        [TestMethod]
        public void ShouldGetAllOrders()
        {
            var service = new OrderService(_mockOrderRepository.Object);
            var orders = service.GetAlOrders().ToList();

            orders.Count.Should().Be(2);
            orders[0].Should().Be(_order1);
            orders[1].Should().Be(_order2);
        }

        [TestMethod]
        public void ShouldReturnNullWhenOrderAmountLessThanZero()
        {
            _order1.Amount = 0d;
            var service = new OrderService(_mockOrderRepository.Object);
            var order = service.PlaceOrder(_order1, 1);
            order.Should().BeNull();
        }

        [TestMethod]
        public void ShouldGetAllCustomersWithLinkedOrders()
        {
            var service = new OrderService(_mockOrderRepository.Object);
            var customers = service.GetAlCustomers().ToList();

            customers.Count.Should().Be(1);
            customers[0].Should().Be(_customer);
            customers[0].Orders.Count.Should().Be(2);
            customers[0].Orders.First().Should().Be(_order1);
            customers[0].Orders.Last().Should().Be(_order2);
        }

        private void AssumeOrderIsInitialised()
        {
            _customer = new Customer
            {
                CustomerId = 1,
                Country = "UK",
                DateOfBirth = DateTime.Now.AddYears(-20),
                Name = "tester2"
            };

            _order1 = new Order
            {
                OrderId = 1,
                Amount = 12,
                VAT = 2,
                Customer = _customer
            };

            _order2 = new Order
            {
                OrderId = 2,
                Amount = 122,
                VAT = 4,
                Customer = _customer
            };

            _orders = new List<Order>
            {
                _order1,
                _order2
            };
            _customer.Orders = _orders;
        }

        private void AssumeRepositoryIsInitialised()
        {
            _mockOrderRepository = new Mock<IOrderRepository>();
        }

        private void AssumRepositoryIsSetup()
        {
            _mockOrderRepository.Setup(m => m.Save(It.IsAny<Order>()));
            _mockOrderRepository.Setup(m => m.GetAllOrders()).Returns(_orders);
            _mockOrderRepository.Setup(m => m.GetCustomer(It.IsAny<int>())).Returns(_customer);
            _mockOrderRepository.Setup(m => m.GetAllCustomers()).Returns(new List<Customer> { _customer });
        }

        private void AssumeDbContextIsInitialised()
        {
            _mockOrderDbSet = new Mock<DbSet<Order>>();
            _mockOrderDbContext = new Mock<OrdersDbContext>();
        }

        private void AssumDbContextIsSetup()
        {
            _mockOrderDbContext.Setup(m => m.Orders).Returns(_mockOrderDbSet.Object);
        }
    }
}
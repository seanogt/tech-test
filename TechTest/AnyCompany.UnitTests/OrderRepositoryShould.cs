using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AnyCompany.Domain;
using AnyCompany.Repository;
using AnyCompany.Services.Repositories;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AnyCompany.UnitTests
{
    [TestClass]
    public class OrderRepositoryShould
    {
        private Order _order1;
        private Order _order2;
        private Customer _customer;
        private IQueryable<Order> _orders;
        private IQueryable<Customer> _customers;
        private Mock<DbSet<Order>> _mockOrderDbSet;
        private Mock<OrdersDbContext> _mockOrderDbContext;
        private Mock<DbSet<Customer>> _mockCustomerDbSet;

        public OrderRepositoryShould()
        {
            AssumeOrderIsInitialised();
            AssumeDbContextIsInitialised();
        }

        [TestMethod]
        public void ShouldSaveOrderOk()
        {
            AssumDbContextIsSetup();

            var repository = new OrderRepository(_mockOrderDbContext.Object);
            repository.Save(_order1);

            _mockOrderDbSet.Verify(m => m.Add(It.IsAny<Order>()), Times.Once());
            _mockOrderDbContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void ShouldSaveCustomerOk()
        {
            AssumDbContextIsSetup();

            var repository = new OrderRepository(_mockOrderDbContext.Object);
            repository.Save(_customer);

            _mockCustomerDbSet.Verify(m => m.Add(It.IsAny<Customer>()), Times.Once());
            _mockOrderDbContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void ShouldGetAllOrders()
        {
            AssumeIQueryableDbSetIsSetup();

            var reposity = new OrderRepository(_mockOrderDbContext.Object);
            var orders = reposity.GetAllOrders().ToList();

            orders.Count.Should().Be(2);
            orders[0].Should().Be(_order1);
            orders[1].Should().Be(_order2);
        }

        [TestMethod]
        public void ShouldGetCustomerOk()
        {
            AssumeIQueryableDbSetIsSetup();

            var repository = new OrderRepository(_mockOrderDbContext.Object);
            var customer = repository.GetCustomer(1);

            customer.Should().NotBeNull();
            customer.Should().Be(_customer);
        }

        [TestMethod]
        public void ShouldGetAllCustomersWithLinkedOrders()
        {
            AssumeIQueryableDbSetIsSetup();

            var reposity = new OrderRepository(_mockOrderDbContext.Object);
            var customers = reposity.GetAllCustomers().ToList();

            customers.Count.Should().Be(1);
            customers[0].Should().Be(_customer);
            customers[0].Orders.Count.Should().Be(2);
            customers[0].Orders.First().Should().Be(_order1);
            customers[0].Orders.Last().Should().Be(_order2);
        }

        private void AssumeIQueryableDbSetIsSetup()
        {
            _mockOrderDbSet.As<IQueryable<Order>>().Setup(m => m.Provider).Returns(_orders.Provider);
            _mockOrderDbSet.As<IQueryable<Order>>().Setup(m => m.Expression).Returns(_orders.Expression);
            _mockOrderDbSet.As<IQueryable<Order>>().Setup(m => m.ElementType).Returns(_orders.ElementType);
            _mockOrderDbSet.As<IQueryable<Order>>().Setup(m => m.GetEnumerator()).Returns(_orders.GetEnumerator());
            _mockOrderDbContext.Setup(c => c.Orders).Returns(_mockOrderDbSet.Object);

            _mockCustomerDbSet.As<IQueryable<Customer>>().Setup(m => m.Provider).Returns(_customers.Provider);
            _mockCustomerDbSet.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(_customers.Expression);
            _mockCustomerDbSet.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(_customers.ElementType);
            _mockCustomerDbSet.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(_customers.GetEnumerator());
            _mockOrderDbContext.Setup(c => c.Customers).Returns(_mockCustomerDbSet.Object);
        }

        private void AssumeOrderIsInitialised()
        {
            _customer = new Customer
            {
                CustomerId = 1,
                Country = "UK",
                DateOfBirth = DateTime.Now.AddYears(-20),
                Name = "tester1"
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
            }.AsQueryable();
            _customer.Orders = new List<Order> { _order1, _order2 };
            _customers = new List<Customer> { _customer }.AsQueryable();
        }

        private void AssumeDbContextIsInitialised()
        {
            _mockOrderDbSet = new Mock<DbSet<Order>>();
            _mockCustomerDbSet = new Mock<DbSet<Customer>>();
            _mockOrderDbContext = new Mock<OrdersDbContext>();
        }

        private void AssumDbContextIsSetup()
        {
            _mockOrderDbContext.Setup(m => m.Orders).Returns(_mockOrderDbSet.Object);
            _mockOrderDbContext.Setup(m => m.Customers).Returns(_mockCustomerDbSet.Object);
        }
    }
}
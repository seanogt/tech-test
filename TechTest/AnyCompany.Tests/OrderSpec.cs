using AnyCompany.DataAccessLayer;
using AnyCompany.Models;
using AnyCompany.RepositoryLayer.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.Tests
{
    [TestClass]
    class OrderSpec
    {
        private Mock<CompanyContext> contextMock;
        private Mock<IOrderRepository<Order>> mockOrderRepository;

        [TestInitialize]
        public void Init()
        {
            contextMock = new Mock<CompanyContext>();
            mockOrderRepository = new Mock<IOrderRepository<Order>>(contextMock.Object);

            var mockCustomer = new List<Customer>
            {
                new Customer
                {
                    Id = 1,
                    Name = "Senzo",
                    Surname = "Meyiwa",
                    Email = "senzomeyiwa@gmail.com",
                    DateOfBirth = DateTime.Now,
                    Country = "UK"
                },
                new Customer
                {
                    Id = 2,
                    Name = "Sipho",
                    Surname = "Khumalo",
                    Email = "siphokhumalo@gmail.com",
                    DateOfBirth = DateTime.Now,
                    Country = "ZAF"
                }
            }.AsQueryable();

            var mockOrder = new List<Order>
            {
                new Order
                {
                    Id = 1,
                    Amount = 200,
                    OrderNumber = "ABCDEF1234",
                    VAT = 0
                },
                new Order
                {
                    Id = 2,
                    Amount = 500,
                    OrderNumber = "ABCDEF0123",
                    VAT = 0
                }
            }.AsQueryable();

            var mockCustomerOrder = new List<CustomerOrder>
            {
                new CustomerOrder
                {
                    Id = 1,
                    CustomerId = 1,
                    OrderId = 1,
                    Completed = false
                },
                new CustomerOrder
                {
                    Id = 1,
                    CustomerId = 1,
                    OrderId = 2,
                    Completed = false
                }
            }.AsQueryable();

            contextMock.Setup(c => c.Customers).Returns((DbSet<Customer>)mockCustomer);
            contextMock.Setup(c => c.Orders).Returns((DbSet<Order>)mockOrder);
            contextMock.Setup(c => c.CustomerOrders).Returns((DbSet<CustomerOrder>)mockCustomerOrder);
        }

        [TestMethod]
        public void GivenAValidCustomerId_WhenSearchingForCustomerOrders_ReturnCustomerOrders()
        {
            var order = Substitute.For<IOrderRepository<Order>>();
            var result = order.GetCustomerOrders(1);

            Assert.AreEqual("Senzo", result.Name);
            Assert.AreEqual(2, result.Orders.Count());
        }

        [TestMethod]
        public void GivenAnInvalidCustomerId_WhenSearchingForCustomerOrders_DoNotReturnCustomerOrders()
        {
            var order = Substitute.For<IOrderRepository<Order>>();
            var result = order.GetCustomerOrders(3);

            Assert.AreEqual("", result.Name);
            Assert.AreEqual("", result.Email);
        }
    }
}

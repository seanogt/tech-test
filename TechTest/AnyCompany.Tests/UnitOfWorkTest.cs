using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AnyCompany.Context;
using AnyCompany.Entity;
using AnyCompany.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AnyCompany.Tests
{
    [TestClass]
    public class UnitOfWorkTest
    {
        private Mock<CustomerDbContext> mockContext = new Mock<CustomerDbContext>();

        private static Mock<DbSet<T>> CreateDbSetMock<T>(IEnumerable<T> elements) where T : class
        {
            var elementsAsQueryable = elements.AsQueryable();
            var dbSetMock = new Mock<DbSet<T>>();

            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(elementsAsQueryable.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(elementsAsQueryable.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(elementsAsQueryable.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(elementsAsQueryable.GetEnumerator());

            return dbSetMock;
        }

        [TestMethod]
        [TestCategory(" Unit Of Work: Positive Scenario only")]
        public void Customers_ReturnAllCustomer()
        {
            // Arrange
            var data = new List<Customer>
            {
               new Customer(){
                   CustomerId = 1,
                   Country = "UK",
                   Name = "UKCustomer",
                   DateOfBirth = new DateTime(1970, 12, 23) }
            };

            mockContext.Setup(s => s.Customers).Returns(CreateDbSetMock<Customer>(data).Object);

            UnitOfWork unitOfWork = new UnitOfWork(mockContext.Object);

            // Act
            var customers = unitOfWork.Customers;

            // Assert
            mockContext.Verify(v => v.Customers, Times.Once());
            Assert.AreEqual(data.Count(), customers.Count);
        }

        [TestMethod]
        [TestCategory(" Unit Of Work: Positive Scenario only")]
        public void Orders_ReturnAllOrder()
        {
            // Arrange
            var data = new List<Order>
            {
               new Order(){
                   CustomerId = 1,
                   OrderId = 1,
                   Amount=1d,
                   VAT=0 }
            };

            mockContext.Setup(s => s.Orders).Returns(CreateDbSetMock<Order>(data).Object);

            UnitOfWork unitOfWork = new UnitOfWork(mockContext.Object);

            // Act
            var orders = unitOfWork.Orders;

            // Assert
            mockContext.Verify(v => v.Orders, Times.Once());
            Assert.AreEqual(data.Count(), orders.Count);
        }


        [TestMethod]
        [TestCategory(" Unit Of Work: Positive Scenario only")]
        public void VATs_ReturnAllVat()
        {
            // Arrange
            var data = new List<VAT>
            {
               new VAT(){
                   Country="UK",
                   ApplyVat=0.2d
               }
            };

            mockContext.Setup(s => s.Vats).Returns(CreateDbSetMock<VAT>(data).Object);

            UnitOfWork unitOfWork = new UnitOfWork(mockContext.Object);

            // Act
            var vats = unitOfWork.VATs;

            // Assert
            mockContext.Verify(v => v.Vats, Times.Once());
            Assert.AreEqual(data.Count(), vats.Count);
        }

        [TestMethod]
        [TestCategory(" Unit Of Work: Positive Scenario only")]
        public void CustomerOrders_LoadAllCustomersAndTheirLinkedOrders()
        {
            // Arrange
            var customer = new List<Customer>
            {
               new Customer(){
                   CustomerId = 1,
                   Country = "UK",
                   Name = "UKCustomer",
                   DateOfBirth = new DateTime(1970, 12, 23) },
                new Customer(){
                   CustomerId = 2,
                   Country = "US",
                   Name = "USCustomer",
                   DateOfBirth = new DateTime(1970, 12, 20) }
            };
            mockContext.Setup(s => s.Customers).Returns(CreateDbSetMock<Customer>(customer).Object);

            var order = new List<Order>
            {
               new Order(){
                   CustomerId = 1,
                   OrderId = 1,
                   Amount=1d,
                   VAT=0 }
            };
            mockContext.Setup(s => s.Orders).Returns(CreateDbSetMock<Order>(order).Object);

            UnitOfWork unitOfWork = new UnitOfWork(mockContext.Object);

            // Act
            var customerOrders = unitOfWork.CustomerOrders;

            // Assert
            mockContext.Verify(v => v.Customers, Times.Once());
            mockContext.Verify(v => v.Orders, Times.Once());
            
            // Total number of customers returned
            Assert.AreEqual(customer.Count(), customerOrders.Count);
            
            // Total number of order for customer 1
            Assert.AreEqual(1, customerOrders.SingleOrDefault(c=>c.Customer.CustomerId==1).Orders.Count);

            // Total number of order for customer 2
            Assert.AreEqual(0, customerOrders.SingleOrDefault(c => c.Customer.CustomerId == 2).Orders.Count);
        }

    }
}

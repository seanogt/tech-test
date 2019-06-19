using System;
using System.Collections.Generic;
using AnyCompany.Entity;
using AnyCompany.Repository;
using AnyCompany.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AnyCompany.Tests
{
    [TestClass]
    public class OrderServiceTest
    {
        private List<Customer> CustomerList = new List<Customer>();
        private List<VAT> VatList = new List<VAT>();

        private Mock<IUnitOfWork> unitOfWork = new Mock<IUnitOfWork>();

        public OrderServiceTest()
        {
            CustomerList.Add(
                new Customer()
                {
                    CustomerId = 1,
                    Country = "UK",
                    Name = "UKCustomer",
                    DateOfBirth = new DateTime(1970, 12, 23)
                });
            CustomerList.Add(
                new Customer()
                {
                    CustomerId = 2,
                    Country = "US",
                    Name = "USCustomer",
                    DateOfBirth = new DateTime(1970, 12, 24)
                });

            unitOfWork.Setup(s => s.Customers).Returns(CustomerList);

            // Add items to the VAT list
            VatList.Add(
                new VAT()
                {
                    ApplyVat = .2d,
                    Country = "UK"
                });
            VatList.Add(new VAT()
            {
                ApplyVat = 0d,
                Country = "US"
            });

            unitOfWork.Setup(s => s.VATs).Returns(VatList);
            unitOfWork.Setup(s => s.Orders).Returns(new List<Order>());
            unitOfWork.Setup(s => s.SaveChangesAsync());
        }

        [TestMethod]
        [TestCategory("Order Service: Positive Scenario - UK Customer")]
        public void Save_SaveAnOrderForUKCustomer()
        {
            // Arrange
            bool expectedOutput = true;
            var order = new Order() { CustomerId = 1, OrderId = 1, Amount = 1d, VAT = 0 };
            OrderService orderRepository = new OrderService(unitOfWork.Object);

            // Act
            var actualOutput = orderRepository.PlaceOrder(order);

            // Assert
            unitOfWork.Verify(v => v.Customers, Times.Once);
            unitOfWork.Verify(v => v.VATs, Times.Once);
            unitOfWork.Verify(v => v.Orders, Times.Once);
            unitOfWork.Verify(v => v.SaveChangesAsync(), Times.Once);
            Assert.AreEqual(expectedOutput, actualOutput);
        }


        [TestMethod]
        [TestCategory("Order Service: Positive Scenario - US Customer")]
        public void Save_SaveAnOrderForUSCustomer()
        {
            // Arrange
            bool expectedOutput = true;
            var order = new Order() { CustomerId = 2, OrderId = 2, Amount = 1d, VAT = 0 };
            OrderService orderRepository = new OrderService(unitOfWork.Object);

            // Act
            var actualOutput = orderRepository.PlaceOrder(order);

            // Assert
            unitOfWork.Verify(v => v.Customers, Times.Once);
            unitOfWork.Verify(v => v.VATs, Times.Once);
            unitOfWork.Verify(v => v.Orders, Times.Once);
            unitOfWork.Verify(v => v.SaveChangesAsync(), Times.Once);
            Assert.AreEqual(expectedOutput, actualOutput);
        }



        [TestMethod]
        [TestCategory("Order Service: Positive Scenario only")]
        public void IsValidOrder_ReturnTrue()
        {
            // Arrange
            bool expectedOutput = true;
            var order = new Order() { CustomerId = 2, OrderId = 2, Amount = 1d, VAT = 0 };
            OrderService orderRepository = new OrderService(unitOfWork.Object);

            // Act
            var actualOutput = orderRepository.IsValidOrder(order);

            // Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }
    }
}

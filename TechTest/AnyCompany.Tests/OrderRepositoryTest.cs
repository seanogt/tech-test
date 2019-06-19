using AnyCompany.Entity;
using AnyCompany.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace AnyCompany.Tests
{
    [TestClass]
    public class OrderRepositoryTest
    {
        private Mock<IUnitOfWork> unitOfWork = new Mock<IUnitOfWork>();

        public OrderRepositoryTest()
        {
            unitOfWork.Setup(s => s.Orders).Returns(new List<Order>());
            unitOfWork.Setup(s => s.SaveChangesAsync());
        }

        [TestMethod]
        [TestCategory("Order Repository: Positive Scenario only")]
        public void Save_SaveAnOrder()
        {
            // Arrange
            var order = new Order() { CustomerId = 1, OrderId = 1, Amount = 1d, VAT = 0 };
            OrderRepository orderRepository = new OrderRepository(unitOfWork.Object);

            // Act
            orderRepository.Save(order);

            // Assert
            unitOfWork.Verify(v => v.Orders, Times.Once);
            unitOfWork.Verify(v => v.SaveChangesAsync(), Times.Once);
        }
    }
}

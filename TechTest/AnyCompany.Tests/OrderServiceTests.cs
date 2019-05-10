using NUnit.Framework;
using System;
using Moq;
using AnyCompany.Repositories;
using AnyCompany.CustomExceptions;
using System.Collections.Generic;
using System.Linq;

namespace AnyCompany.Tests
{
    [TestFixture()]
    public class OrderServiceTests
    {
        readonly int NON_EXISTENT_CUSTOMER_ID = 1;
        readonly int EXISTING_CUSTOMER_ID = 2;

        readonly Customer NON_UK_CUSTOMER = new Customer() { Name = "John", Country = "Sweden", DateOfBirth = new DateTime(1971, 7, 22), CustomerId=2 };
        readonly Customer UK_CUSTOMER = new Customer() { Name = "Jane", Country = "UK", DateOfBirth = new DateTime(1981, 4, 13), CustomerId=3 };
       

        readonly Order VALID_ORDER = new Order() { OrderId = 1, Amount = 5, VAT = 0.5d, CustomerId=2 };
        readonly Order ANOTHER_VALID_ORDER = new Order() { OrderId = 3, Amount = 75, VAT = 0.3d, CustomerId=3 };
        readonly     Order INVALID_ZERO_AMOUNT_ORDER = new Order() { OrderId = 2, Amount = 0, VAT = 0.1d, CustomerId=2 };

        private OrderService service;
        
       

        [SetUp]
        public void Setup()
        {
            service = new TestableOrderService();
        }


        [Test()]
        public void GivenNonExistentCustomerId_PlaceOrder_ShouldThrowNullReferenceException()
        {
            Assert.Throws<NullReferenceException>(() => service.PlaceOrder(VALID_ORDER, NON_EXISTENT_CUSTOMER_ID));
        }

        [Test()]
        public void GivenExistingCustomerId_PlaceOrder_ShouldNotThrow()
        {
            Assert.DoesNotThrow(()=> service.PlaceOrder(VALID_ORDER, EXISTING_CUSTOMER_ID));
        }

        //[Test()]
        //public void GivenZeroAmountOrder_PlaceOrder_ShouldThrowInvalidOrderException()
        //{
        //    Assert.Throws<InvalidOrderException>(() => service.PlaceOrder(INVALID_ZERO_AMOUNT_ORDER, EXISTING_CUSTOMER_ID));
        //}

        [Test()]
        public void GivenZeroAmountOrder_PlaceOrder_ShouldReturnFalse()
        {
            Assert.That(service.PlaceOrder(INVALID_ZERO_AMOUNT_ORDER, EXISTING_CUSTOMER_ID), Is.False);
        }

        [Test()]
        public void GetOrders_ShouldReturn2Orders()
        {
            var orders = new List<Order>();
            orders.Add(VALID_ORDER);
            orders.Add(ANOTHER_VALID_ORDER);
            
            var orderRepoMock = new Mock<IOrderRepository>();
            orderRepoMock.Setup(p => p.GetOrders()).Returns(orders);

            var customerRepoMock = new Mock<ICustomerRepository>();

            service = new TestableOrderService(orderRepoMock.Object, customerRepoMock.Object);

            var result = service.GetOrders();

            Assert.That(result.Count() == 2);

        }

        [Test()]
        public void GivenACustomerId_GetOrdersByCustomerId_ShouldReturnCorrectOrder()
        {
            var orders = new List<Order>();
            orders.Add(VALID_ORDER);
            orders.Add(ANOTHER_VALID_ORDER);

            var orderRepoMock = new Mock<IOrderRepository>();
            orderRepoMock.Setup(p => p.GetOrders()).Returns(orders);

            var customerRepoMock = new Mock<ICustomerRepository>();

            service = new TestableOrderService(orderRepoMock.Object, customerRepoMock.Object);

            var result = service.GetOrdersByCustomerId(EXISTING_CUSTOMER_ID);

            Assert.That(result.SingleOrDefault().Amount == 5);

        }


        private class TestableOrderService : OrderService
        {

            public TestableOrderService()
            { }

            public TestableOrderService(IOrderRepository orderRepo, ICustomerRepository customerRepo) : base(orderRepo, customerRepo)
            {

            }

            protected override Customer LoadACustomerFromCustomerRepository(int customerId)
            {
                if (customerId == 1)
                    return null;
                else
                    return new Customer();
            }

            protected override void SaveOrderToOrderRepository(Order order)
            {
                var mockOrderRepository = new Mock<IOrderRepository>();
                mockOrderRepository.Object.Save(order);
            }

        }
    }
}

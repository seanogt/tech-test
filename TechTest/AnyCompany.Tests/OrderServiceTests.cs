using NUnit.Framework;
using System;
using Moq;
using AnyCompany.Repositories;

namespace AnyCompany.Tests
{
    [TestFixture()]
    public class OrderServiceTests
    {
        readonly Order VALID_ORDER = new Order() { OrderId = 1, Amount = 5, VAT = 0.5d };
        readonly int NON_EXISTENT_CUSTOMER_ID=1;

        private int EXISTING_CUSTOMER_ID = 2;

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


        private class TestableOrderService : OrderService
        {

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

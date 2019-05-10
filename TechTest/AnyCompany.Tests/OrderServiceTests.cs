using NUnit.Framework;
using System;

namespace AnyCompany.Tests
{
    [TestFixture()]
    public class OrderServiceTests
    {
        readonly Order VALID_ORDER = new Order() { OrderId = 1, Amount = 5, VAT = 0.5d };
        readonly int NON_EXISTENT_CUSTOMER_ID=1;

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


        private class TestableOrderService : OrderService
        {

            protected override Customer LoadACustomerFromCustomerRepository(int customerId)
            {
                return null;
            }

        }
    }
}

using NUnit.Framework;
using System;
using Moq;
using AnyCompany.Repositories;
using AnyCompany.CustomExceptions;

namespace AnyCompany.Tests
{
    [TestFixture()]
    public class OrderServiceTests
    {
        readonly Order VALID_ORDER = new Order() { OrderId = 1, Amount = 5, VAT = 0.5d };
        readonly int NON_EXISTENT_CUSTOMER_ID=1;

        readonly int EXISTING_CUSTOMER_ID = 2;
        readonly     Order INVALID_ZERO_AMOUNT_ORDER = new Order() { OrderId = 2, Amount = 0, VAT = 0.1d };

        private OrderService service;
        readonly Customer NON_UK_CUSTOMER= new Customer() { Name = "John", Country = "Sweden", DateOfBirth = new DateTime(1971, 7, 22) };
        readonly Customer UK_CUSTOMER = new Customer() { Name = "Jane", Country = "UK", DateOfBirth = new DateTime(1981, 4, 13) };
       
       

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
        public void GivenNonUKCustomer_SetVAT_ShouldSetVATToZero()
        {
            var result = service.SetVAT(VALID_ORDER, NON_UK_CUSTOMER);
            Assert.That(result.VAT ==0);
        }

        [Test()]
        public void GivenAUKCustomer_SetVAT_ShouldSetVATTo2Percents()
        {
            var result = service.SetVAT(VALID_ORDER, UK_CUSTOMER);
            Assert.That(result.VAT == 0.2d);
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

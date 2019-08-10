using System;
using AnyCompany.IntegrationTests.DataHelpers;
using AnyCompany.Ioc;
using AnyCompany.Models;
using AnyCompany.Services.Constants;
using AnyCompany.Services.Dtos;
using AnyCompany.Services.Services;
using Castle.Windsor;
using NUnit.Framework;

namespace AnyCompany.IntegrationTests
{
    [TestFixture]
    public class OrderServiceIntegrationTests
    {
        private IWindsorContainer _container;

        [SetUp]
        public void SetupFixture()
        {
            _container = Bootstrapper.GetContainer();
        }

        [TestCase("UK", VatConstants.UkVat)]
        [TestCase("FR", VatConstants.RowVat)]
        public void PlaceOrder_ShouldInsertAnOrder_AndReturnTrue(string country, double expectedVat)
        {
            // Arrange.
            var customer = CustomerDataHelper.Add(GetCustomer(country));
            var order = GetOrderDto();
            var orderService = _container.Resolve<IOrderService>();

            // Act.
            var result = orderService.PlaceOrder(order, customer.CustomerId);
            var insertedOrder = OrderDataHelper.GetOrder(order.OrderId);

            // Assert.
            Assert.IsTrue(result);
            Assert.IsNotNull(insertedOrder);
            Assert.AreEqual(customer.CustomerId, insertedOrder.CustomerId);
            Assert.AreEqual(order.OrderId, insertedOrder.OrderId);
            Assert.AreEqual(order.Amount, insertedOrder.Amount);
            Assert.AreEqual(expectedVat, insertedOrder.VAT);
        }

        private OrderDto GetOrderDto()
        {
            return new OrderDto
            {
                OrderId = new Random().Next(1, 200000000), // generate a random Id since the OrderId column is not an IDENTITY column
                Amount = 200.99d
            };
        }

        private Customer GetCustomer(string country)
        {
            return new Customer
            {
                Country = country,
                DateOfBirth = DateTime.UtcNow.AddYears(-41),
                Name = "John Smith"
            };
        }
    }
}

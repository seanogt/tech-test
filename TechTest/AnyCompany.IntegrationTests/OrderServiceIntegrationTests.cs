using System;
using AnyCompany.IntegrationTests.DataHelpers;
using AnyCompany.Models;
using AnyCompany.Services.Dtos;
using NUnit.Framework;

namespace AnyCompany.IntegrationTests
{
    [TestFixture]
    public class OrderServiceIntegrationTests
    {
        [TestCase("UK")]
        [TestCase("FR")]
        public void PlaceOrder_ShouldInsertAnOrder_AndReturnTrue(string country)
        {
            // Arrange.
            var customer = CustomerDataHelper.Add(GetCustomer(country));
            var order = GetOrderDto();
            var orderService = new OrderService();

            // Act.
            var result = orderService.PlaceOrder(order, customer.CustomerId);
            var insertedOrder = OrderDataHelper.GetOrder(order.OrderId);

            // Assert.
            Assert.IsTrue(result);
            Assert.IsNotNull(insertedOrder);
        }

        private OrderDto GetOrderDto()
        {
            return new OrderDto
            {
                OrderId = new Random().Next(1, 2000000), // generate a random Id since the OrderId column is not an IDENTITY column
                Amount = 200.99
            };
        }

        private Customer GetCustomer(string country)
        {
            return new Customer
            {
                Country = country,
                DateOfBirth = DateTime.UtcNow.AddDays(-20),
                Name = "John Smith"
            };
        }
    }
}

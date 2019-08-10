
using System;
using System.Linq;
using AnyCompany.IntegrationTests.DataHelpers;
using AnyCompany.Ioc;
using AnyCompany.Models;
using AnyCompany.Services.Services;
using Castle.Windsor;
using NUnit.Framework;

namespace AnyCompany.IntegrationTests
{
    [TestFixture]
    public class CustomerServiceIntegrationTests
    {
        private IWindsorContainer _container;

        [SetUp]
        public void SetupFixture()
        {
            _container = Bootstrapper.GetContainer();
        }

        [Test]
        public void GetAllCustomersWithOrders_ShouldReturnCustomersAndOrders()
        {
            // Arrange.
            var customerOne = CustomerDataHelper.Add(GetCustomer("UK"));
            var orderOne = OrderDataHelper.AddOrder(GetOrder(customerOne.CustomerId));
            var orderTwo = OrderDataHelper.AddOrder(GetOrder(customerOne.CustomerId));

            var customerTwo = CustomerDataHelper.Add(GetCustomer("FR"));
            var orderThree = OrderDataHelper.AddOrder(GetOrder(customerTwo.CustomerId));

            var customerOrderService = _container.Resolve<ICustomerOrderService>();

            // Act.
            var results = customerOrderService.GetAllCustomerWithOrders();
            var customerOneResult = results.First(c => c.Customer.CustomerId == customerOne.CustomerId);
            var customerTwoResult = results.First(c => c.Customer.CustomerId == customerTwo.CustomerId);

            // Assert.
            Assert.AreEqual(2, customerOneResult.Orders.Count());
            Assert.AreEqual(1, customerTwoResult.Orders.Count());
            Assert.IsTrue(customerOneResult.Orders.Any(o => o.OrderId == orderOne.OrderId));
            Assert.IsTrue(customerOneResult.Orders.Any(o => o.OrderId == orderTwo.OrderId));
            Assert.IsTrue(customerTwoResult.Orders.Any(o => o.OrderId == orderThree.OrderId));
        }

        private Order GetOrder(int customerId)
        {
            return new Order
            {
                OrderId = new Random().Next(1, 200000000),
                Amount = 100,
                CustomerId = customerId,
                VAT = 0
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

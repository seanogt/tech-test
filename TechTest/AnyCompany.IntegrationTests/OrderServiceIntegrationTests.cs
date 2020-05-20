using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AnyCompany.IntegrationTests
{
    [TestClass]
    public class OrderServiceIntegrationTests
    {
        private IOrderService _orderService;

        public OrderServiceIntegrationTests()
        {
            Config config = new Config();

            var customerRepository = new CustomerRepositoryProxy(config.CustomerDbConnectionString);

            var orderRepository = new OrderRepository(config.OrdersDbConnectionString);

            _orderService = new OrderService(orderRepository, customerRepository);
        }

        [TestMethod]
        public void PlaceOrder_Successfully()
        {
            // Arrange          
            Order newOrder = new Order()
            {
                CustomerId = 1,
                OrderId = 100,
                Amount = 100,
                VAT = 100
            };

            // Act
            var isOrderPlaced = _orderService.PlaceOrder(newOrder, 1);

            // Assert
            Assert.AreEqual(true, isOrderPlaced);
        }
    }
}

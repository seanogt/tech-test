using AnyCompany;
using AnyCompany.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AnyCompanyUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestOrderLoad()
        {
            // Arrange 
            Order order = new Order()
            {
                Amount = 1000.00,
                CustomerId = 1,
                OrderId = 1,
                VAT = 1.2
            };
            OrderService service = new OrderService();

            // Act 
            service.PlaceOrder(order); // We can write mock here if we were using iterfaces.

            // Assert
            var customer = service.GetCustomerOrders(order.CustomerId);
            Assert.IsTrue(customer != null);
        }

        [TestMethod]
        public void TestCustomerOrders()
        {
            //  Arrange 
            OrderService service = new OrderService();

            // Act 
            var customerOrders = service.GetCustomerOrders(); // We can write mock here if we were using iterfaces.

            // Assert
            Assert.IsTrue(customerOrders != null);

        }
    }
}

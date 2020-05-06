using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnyCompany.Models;

namespace AnyCompany.Business.Tests
{
    [TestClass()]
    public class OrderServiceTests
    {
        [TestMethod()]
        public void OrderServiceTest()
        {
            OrderService service = new OrderService();

            var testCustomer = service.RetrieveCustomerOrders().Where(c => c.Orders == null || c.Orders.Count == 0).FirstOrDefault();

            if (testCustomer == null)
            {
                testCustomer = service.NewCustomer(new Customer()
                {
                    Name = "Test Customer",
                    DateOfBirth = DateTime.Now.AddYears(-21),
                    Country = "UK"
                });
            }

            for (int i = 1; i <= 5; i++)
            {
                Order order = new Order()
                {
                    CustomerId = testCustomer.CustomerId,
                    Amount = 100 + i
                };

               service.PlaceOrder(order);
            }

            var result = service.RetrieveCustomerOrders();

            int orderCount = result.Where(c => c.CustomerId == testCustomer.CustomerId).FirstOrDefault()?.Orders?.Count() ?? 0;

            Assert.AreEqual(orderCount, 5);
        }
    }
}
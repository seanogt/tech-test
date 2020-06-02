using System;
using AnyCompany.Models;
using AnyCompany.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Anycompany.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var orderService = new OrderService();
            var order = new OrderModel
            {
                Amount = 2,
            };
            Assert.IsTrue(orderService.PlaceOrder(order,1));
        }
    }
}

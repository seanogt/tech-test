using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnyCompany.Interfaces;
using AnyCompany.Models;

namespace AnyCompany.Tests
{
    [TestClass]
    public class ServiceTests
    {
        public static IOrderService orderService;
        [TestMethod]
        public void TestOrderInvalidCustomerId()
        {
            Order newOrder = new Order
            {
                CustomerId = -1,
                Amount = 10,
                OrderId = 11,
                VAT = 0
            };
            var success = orderService.PlaceOrder(newOrder, -1, out string error);
            Assert.AreEqual(success, false);
            Assert.AreEqual(error, "Please provide a correct Customer Id and try again");
        }

        [TestMethod]
        public void TestOrderInvalidAmount()
        {
            Order newOrder = new Order
            {
                CustomerId = 1,
                Amount = 0,
                OrderId = 11,
                VAT = 0
            };
            var success = orderService.PlaceOrder(newOrder, 1, out string error);
            Assert.AreEqual(success, false);
            Assert.AreEqual(error, "Amount can not be zero or below, please provide a correct amount and try again");
        }

        [TestMethod]
        public void TestOrderSuccess()
        {
            Order newOrder = new Order
            {
                CustomerId = 1,
                Amount = 10,
                OrderId = 11,
                VAT = 0
            };
            var success = orderService.PlaceOrder(newOrder, 1, out string error);
            Assert.AreEqual(success, true);
            Assert.AreEqual(error, "");
        }
    }
}

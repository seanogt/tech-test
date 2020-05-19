using AnyCompany.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.Tests
{
    [TestClass]
    public class CustomerOrderTest
    {
        public static IOrderService orderService;

        [TestMethod]
        public void TestOrderInvalidCustomerID()
        {
            Order order = new Order
            {
                CustomerID = -1,
                Amount = 20,
                OrderId = 1,
                VAT = 14
            };
            var success = orderService.PlaceOrder(order, -1, out string message);
            Assert.AreEqual(success, false);
            Assert.AreEqual(message, "Please insert valid customer id");
        }

        [TestMethod]
        public void TestOrderInvalidAmount()
        {
            Order order = new Order
            {
                CustomerID = 1,
                Amount = 20,
                OrderId = 1,
                VAT = 14
            };
            var success = orderService.PlaceOrder(order, 1, out string message);
            Assert.AreEqual(success, false);
            Assert.AreEqual(message, "Amount can not be zero or below, please provide a correct amount and try again");
        }

        [TestMethod]
        public void TestOrderSuccess()
        {
            Order order = new Order
            {
                CustomerID = 1,
                Amount = 20,
                OrderId = 1,
                VAT = 14
            };
            var success = orderService.PlaceOrder(order, 1, out string message);
            Assert.AreEqual(success, true);
            Assert.AreEqual(message, "");
        }

    }
}

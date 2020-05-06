using AnyCompany.Models;
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
        [TestMethod]
        public void RetrieveOrderTest()
        {
            OrderService service = new OrderService();

            Order order = new Order()
            {
                Amount = 100,
                VAT = 15
            };

            var result = service.RetrieveCustomerOrders();

            Assert.IsNotNull(result);
        }
    }
}

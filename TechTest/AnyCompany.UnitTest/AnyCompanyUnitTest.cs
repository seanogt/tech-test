using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AnyCompany.UnitTest
{
    [TestClass]
    public class AnyCompanyUnitTest
    {
        private readonly string testId = "c26f9aca-97e5-405a-b75b-bb1126850244";

        [TestMethod]
        public void LoadCustomer_Test()
        {
            Guid.TryParse(testId, out var custid);
            var cust = CustomerRepository.Load(custid);
            Assert.IsNotNull(cust);
        }


        [TestMethod]
        public void PlaceOrderTest()
        {
            var orderService = new OrderService();
            Guid.TryParse(testId, out var custid);
            var order = new Order() { Amount = 124.4 };

            var orderVerfify = orderService.PlaceOrder(order, custid);
            Assert.IsTrue(orderVerfify);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnyCompany.Interface;

namespace AnyCompany.Tests
{
    public class Class1
    {
        private string customerId = "c26f9aca-97e5-405a-b75b-bb1126850244";

        public void LoadCustomerTest()
        {
            Guid.TryParse(customerId, out var custid);
            CustomerRepository.Load(custid);
        }

        public void PlaceOrderTest()
        {
            Guid.TryParse(customerId, out var custid);
            var orderService = new OrderService();
            var orderVerify = orderService.PlaceOrder(new Order() { Amount = 123.34 }, custid);
            Console.WriteLine(orderVerify ? "Order successful" : "Order unsuccessful");
        }
    }
}

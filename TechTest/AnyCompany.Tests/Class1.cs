using System;

using AnyCompany.Models;
using AnyCompany.Repositories;
using AnyCompany.Services;

namespace AnyCompany.Tests
{
    public class Class1
    {
        private readonly string customerId = "c26f9aca-97e5-405a-b75b-bb17686606";

        public void LoadCustomerTest()
        {
            Guid.TryParse(customerId, out var cusId);
            CustomerRepository.Load(cusId);
        }

        public void PlaceOrderTest()
        {
            Guid.TryParse(customerId, out var cusId);
            var orderService = new OrderService();
            var orderVerify = orderService.PlaceOrder(new Order() { Amount = 59.89 }, cusId);
            Console.WriteLine((bool)orderVerify ? "Order successful" : "Order unsuccessful");
        }
    }
}
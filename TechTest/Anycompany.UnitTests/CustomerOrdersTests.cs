using System;
using AnyCompany.DataRepositories;
using AnyCompany.Models;
using AnyCompany.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Anycompany.UnitTests
{
    [TestClass]
    public class CustomerOrdersTests
    {

        [TestMethod]
        public void TestCustomerLoad()
        {
            var customer = CustomerRepository.Load(1);
            Assert.IsNotNull(customer);
        }

        [TestMethod]
        public void TestOrderAdd()
        {
            var orderService = new OrderService();
            var order = new OrderModel
            {
                Amount = 2,
                CustomerId = 1,
                OrderId =  4
            };
            Assert.IsTrue(orderService.PlaceOrder(order,1));
        }

        [TestMethod]
        public void TestGetAllCustomerOrders()
        {
            var customerService = new CustomerService();

            Assert.IsNotNull(customerService.GetAllOrdersForAllCustomers());
        }
        
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnyCompany;


namespace AnyCompany.Tests
{
    [TestClass()]
    public class Class1
    {
        [TestMethod()]
        public void AddCustomer()
        {
            Customer cust = new Customer
            {
                CustomerId = 3,
                Name = "Kamo",
                Country = "Ghana",
                DateOfBirth = DateTime.Now

            };

            int count = CustomerRepository.Save(cust);

            bool result = false;

            if (count > 0)
            {
                result = true;
            }

            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void PlaceOrder()
        {
            Order item = new Order
            {
                OrderId = 3,
                Amount = 700,
                VAT = 15,
                CustomerId = 2
            };

            OrderService orderService = new OrderService();

            bool result = orderService.PlaceOrder(item, item.CustomerId);

            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void GetCustomerOrders()
        {
            Customer cust = CustomerRepository.GetCustomerOrders(2);

            if (cust.CustomerId == 0)
            {
                Assert.Fail("Customer Does not exist");
            }

            if (cust.Orders.Count == 0)
            {
                Assert.Fail("Customer Does not have any orders assigned");
            }
            
        }
    }
}

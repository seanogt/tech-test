using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AnyCompany.UnitTest
{
    [TestClass]
    public class UnitTestAnyCompany
    {
        [TestMethod]
        public void TestTableCreate()
        {
            Repository.Context.CheckAllTableExist();
        }
        [TestMethod]
        public void TestCreateCustomerNoCountry()
        {
            string message = "";
            Entities.Customer customer = new Entities.Customer()
            {
                DateOfBirth = new DateTime(1987, 11, 23),
                Name = "HJB",
            };

            Service.CustomerService.Save(customer, out message);

            Assert.AreEqual("Please select a Country.", message);
        }

        [TestMethod]
        public void TestCreateCustomerInvalidDOB()
        {
            string message = "";
            Entities.Customer customer = new Entities.Customer()
            {
                Country = "US",
                DateOfBirth = new DateTime(1800, 11, 23),
                Name = "HJB",
            };

            Service.CustomerService.Save(customer, out message);

            Assert.AreEqual("Please select a valid Date Of Birth.", message);
        }

        [TestMethod]
        public void TestCreateCustomerNoName()
        {
            string message = "";
            Entities.Customer customer = new Entities.Customer()
            {
                Country = "US",
                DateOfBirth = new DateTime(1987,11,23),                
            };

            Service.CustomerService.Save(customer, out message);
   
            Assert.AreEqual("Please enter a name.", message);
        }

        [TestMethod]
        public void TestCreateCustomer()
        {
            string message = "";
            bool success = true;
            if (success)
            {
                Entities.Customer customer = new Entities.Customer()
                {
                    Country = "US",
                    DateOfBirth = new DateTime(1987, 11, 23),
                    Name = "HJB",
                };

                success = Service.CustomerService.Save(customer, out message);
            }
            if (success)
            {
                Entities.Customer customer = new Entities.Customer()
                {
                    Country = "RSA",
                    DateOfBirth = new DateTime(1987, 11, 23),
                    Name = "CJB",
                };
                success = Service.CustomerService.Save(customer, out message);
            }

            if (success)
            {
                Entities.Customer customer = new Entities.Customer()
                {
                    Country = "RSA",
                    DateOfBirth = new DateTime(1987, 11, 23),
                    Name = "CJB",
                };
                success = Service.CustomerService.Save(customer, out message);
            }

            if (success)
            {
                Entities.Customer customer = new Entities.Customer()
                {
                    Country = "UK",
                    DateOfBirth = new DateTime(1988, 11, 23),
                    Name = "JRBCC",
                };
                success = Service.CustomerService.Save(customer, out message);
            }

            Assert.AreEqual(true, success);
        }

        [TestMethod]
        public void TestLoadAllCustomer()
        {
            List<Entities.Customer> customerList = Service.CustomerService.LoadAll();
        }
        [TestMethod]
        public void TestPlaceOrderInvalidCustomer()
        {
            string message = "";
            Entities.Order order = new Entities.Order()
            {
                CustomerId = -1,
                Amount = 500,
            };

            Service.OrderService.PlaceOrder(order, out message);
            Assert.AreEqual("Please select a customer.", message);

        }
        [TestMethod]
        public void TestPlaceOrderInvalidAmount()
        {
            string message = "";
            Entities.Order order = new Entities.Order()
            {
                CustomerId = 1,
                Amount = 0,
            };

            Service.OrderService.PlaceOrder(order, out message);
            Assert.AreEqual("Amount can't be zero.", message);

        }
        [TestMethod]
        public void TestPlaceOrder()
        {
            string message = "";
            Entities.Order order = new Entities.Order()
            {
                CustomerId = 1,
                Amount = 500,
            };

            Service.OrderService.PlaceOrder(order, out message);
            Assert.AreEqual("", message);

        }

        [TestMethod]
        public void TestPlaceMultiOrders()
        {
            string message = "";
            string messageAll = "";

            List<Entities.Customer> customerList = Service.CustomerService.LoadAll();
            foreach (Entities.Customer customer in customerList)
            {
                for (int i = 0; i <= 5; i++)
                {
                    Entities.Order order = new Entities.Order()
                    {
                        CustomerId = customer.CustomerId,
                        Amount = 500 * customer.CustomerId,
                    };
                    Service.OrderService.PlaceOrder(order, out message);
                    messageAll += message;
                }                
            }
            
            Assert.AreEqual("", messageAll);

        }


        [TestMethod]
        public void TestLoadOrders()
        {
            string message = "";
            Service.OrderService.LoadByCustomer(1, out message);
            Assert.AreEqual("", message);
        }

        [TestMethod]
        public void TestLoadCustomerAndOrders()
        {
            string message = "";
            string messageAll = "";
            List<Entities.Customer> customerList = Service.CustomerService.LoadAll();
            foreach (Entities.Customer customer in customerList)
            {
                Service.OrderService.LoadByCustomer(customer.CustomerId, out message);
                messageAll += message;
            }
            Assert.AreEqual("", messageAll);
        }
    }
}

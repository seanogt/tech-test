using AnyCompany.Models;
using AnyCompany.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.Tests
{
    [TestClass]
    public class TestCustomerService
    {
        private readonly CustomerService _customerService;

        public TestCustomerService()
        {
            _customerService = new CustomerService(
                new CustomerRepositoryMock());
        }

        [TestMethod]
        public void GetCustomerById_ValidCustomerId_ReturnsCustomer()
        {
            ////----Act
            var response = _customerService.GetCustomerById(1);

            ////----Assert
            Assert.IsNotNull(response);
            Assert.IsTrue(response is Customer);
            Assert.AreEqual(1, response.CustomerId);
        }

        [TestMethod]
        public void GetCustomerById_InvalidCustomerId_ReturnsEmptyCustomer()
        {
            ////----Act
            var response = _customerService.GetCustomerById(5);

            ////----Assert
            Assert.IsNotNull(response);
            Assert.IsTrue(response is Customer);
            Assert.AreEqual(0, response.CustomerId);
        }

        [TestMethod]
        public void GetAllCustomers_ReturnsListOfCustomers()
        {
            ////----Act
            var response = _customerService.GetAllCustomers();

            ////----Assert
            Assert.IsNotNull(response);
            Assert.IsTrue(response is List<Customer>);
            Assert.IsTrue(response.Count() > 0);
        }

    }
}

using AnyCompany.Entity;
using AnyCompany.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnyCompany.Tests
{
    [TestClass]
    public class CustomerRepositoryTest
    {
        private List<Customer> CustomerList = new List<Customer>();
        private Mock<IUnitOfWork> unitOfWork = new Mock<IUnitOfWork>();

        public CustomerRepositoryTest()
        {
            CustomerList.Add(
                new Customer()
                {
                    CustomerId = 1,
                    Country = "UK",
                    Name = "UKCustomer",
                    DateOfBirth = new DateTime(1970, 12, 23)
                });
            CustomerList.Add(
                new Customer()
                {
                    CustomerId = 2,
                    Country = "US",
                    Name = "USCustomer",
                    DateOfBirth = new DateTime(1970, 12, 24)
                });

            unitOfWork.Setup(s => s.Customers).Returns(CustomerList);
        }

        [TestMethod]
        [TestCategory("Customer Repository: Positive Scenario only")]
        public void Load_ReturnACustomer()
        {
            // Arrange
            var expectedCustomer = CustomerList.SingleOrDefault<Customer>(c => c.CustomerId == 1);

            // Act
            var customer = CustomerRepository.Load(expectedCustomer.CustomerId, unitOfWork.Object);

            // Assert
            unitOfWork.Verify(v => v.Customers, Times.Once);
            Assert.AreEqual(expectedCustomer.CustomerId, customer.CustomerId);
        }


        [TestMethod]
        [TestCategory("Customer Repository: Positive Scenario only")]
        public void Load_ReturnAllCustomers()
        {
            // Arrange
            var expectedCustomerCount = CustomerList.Count;

            // Act
            var customers = CustomerRepository.LoadAll(unitOfWork.Object).ToList<Customer>();

            // Assert
            unitOfWork.Verify(v => v.Customers, Times.Once);
            Assert.AreEqual(expectedCustomerCount, customers.Count);
        }
    }
}

using AnyCompany.DataAccessLayer;
using AnyCompany.Models;
using AnyCompany.RepositoryLayer;
using AnyCompany.RepositoryLayer.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.Tests
{
    [TestClass]
    public class CustomerSpec
    {
        private Mock<CompanyContext> contextMock;

        [TestInitialize]
        public void Init()
        {
            contextMock = new Mock<CompanyContext>();

            var mockCustomer = new List<Customer>
            {
                new Customer
                {
                    Id = 1,
                    Name = "Senzo",
                    Surname = "Meyiwa",
                    Email = "senzomeyiwa@gmail.com",
                    DateOfBirth = DateTime.Now,
                    Country = "UK"
                }
            }.AsQueryable();

            contextMock.Setup(c => c.Customers).Returns((DbSet<Customer>)mockCustomer);
        }

        [TestMethod]
        public void GivenAValidCustomerId_WhenRequestingCustomerDetails_ReturnCustomerDetails()
        {

            var customer = Substitute.For<ICustomerRepository<Customer>>();
            var result = customer.GetCustomer(1);

            Assert.AreEqual("Senzo",result.Name);
            Assert.AreEqual("Meyiwa", result.Surname);
        }

        [TestMethod]
        public void GivenAnInvalidCustomerId_WhenRequestingCustomerDetails_DontReturnCustomerDetails()
        {
            var customer = Substitute.For<ICustomerRepository<Customer>>();
            var result = customer.GetCustomer(2);

            Assert.AreEqual("", result.Name);
            Assert.AreEqual("", result.Surname);
        }

    }
}

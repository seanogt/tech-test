using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnyCompany.Models;
using AnyCompany.Repositories.CustomerRepository;
using NUnit.Framework;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace AnyCompany.Tests
{
    [TestFixture]
    public class UnitOfWorkTest
    {
        [Test]
        public void Given_Correct_Order_Object_Save_Return_True()
        {
            var unitOfWorkRepo = Substitute.For<IUnitOfWork>();

            var order = new Order
            {
                Amount = 50,
                CustomerId = 1,
                OrderId = 1,
                CustomerAccountNumber = "7KK23",
                VAT = 0,
                Address = new Address
                {
                    HouseNumber = 23,
                    PostalCode = "2001",
                    StreetName = "Ramaphosa Street",
                    Surburb = "Saxonworld",
                    Province = "Gauteng",
                    Country = "South Africa",
                    CustomerId = 1
                }
            };

            var results = unitOfWorkRepo.Save(order);

            Assert.IsTrue(results);
        }

        [Test]
        public void Given_An_Empty_Order_Object_Save_Return_False()
        {
            var unitOfWorkRepo = Substitute.For<IUnitOfWork>();

            var order = new Order
            {

            };

            var results = unitOfWorkRepo.Save(order);

            Assert.IsFalse(results);
        }

        [Test]
        public void Given_A_Correct_CustomerId_GetAllCustomerOrder_Return_Results()
        {
            var unitOfWorkRepo = Substitute.For<IUnitOfWork>();

            var results = unitOfWorkRepo.GetAllCustomerOrders(1);

            Assert.IsNotNull(results);
        }

        [Test]
        public void Given_An_Empty_CustomerId_GetAllCustomerOrder_Return_No_Results()
        {
            var unitOfWorkRepo = Substitute.For<IUnitOfWork>();

            var results = unitOfWorkRepo.GetAllCustomerOrders(0);

            Assert.IsEmpty(results);
        }

        [Test]
        public void Check_If_GetAllOrders_Returns_Results()
        {
            var unitOfWorkRepo = Substitute.For<IUnitOfWork>();

            var results = unitOfWorkRepo.GetAllOrders();

            Assert.IsNotNull(results);
        }

        [Test]
        public void Check_If_GetAllCustomers_Returns_Results()
        {
            var unitOfWorkRepo = Substitute.For<IUnitOfWork>();

            var results = unitOfWorkRepo.GetAllCustomers();

            Assert.IsNotNull(results);
        }
    }
}

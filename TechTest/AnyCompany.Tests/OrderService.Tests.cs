using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using AnyCompany.Models;
using AnyCompany.Repositories.CustomerRepository;
using NSubstitute;

namespace AnyCompany.Tests
{
    [TestFixture]
    public class OrderServiceTests
    {
        [Test]
        public void CheckIf_GetCustomerWithOrders_Returns_None_Empty_results()
        {
            var iunitRepo = Substitute.For<IUnitOfWork>();
            var unitRep = new OrderService(iunitRepo);
            var custorders = unitRep.GetCustomersWithOrders();

            Assert.NotNull(custorders);
        }

        [Test]
        public void Given_Non_Empty_Correct_CustomerId_PlaceOrder_Return_Results()
        {
            var iunitRepo = Substitute.For<IUnitOfWork>();
            var unitRep = new OrderService(iunitRepo);
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
            var placeorderTest = unitRep.PlaceOrder(order, 1);

            Assert.IsNotNull(placeorderTest);
            Assert.IsTrue(placeorderTest);
        }

        [Test]
        public void Given_Invalid_CustomerId_PlaceOrder_Return_False_Results()
        {
            var iunitRepo = Substitute.For<IUnitOfWork>();
            var unitRep = new OrderService(iunitRepo);
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
                    CustomerId = 0
                }
            };
            var placeorderTest = unitRep.PlaceOrder(order, 0);

            Assert.IsNotNull(placeorderTest);
            Assert.IsFalse(placeorderTest);
        }

        [Test]
        public void Given_Invalid_CustomerId_PlaceOrder_Return_No_Results()
        {

            var iunitRepo = Substitute.For<IUnitOfWork>();
            var unitRep = new OrderService(iunitRepo);
            var order = new Order
            {
                Amount = 50,
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
                    Country = "South Africa"
                }
            };
            var placeorderTest = unitRep.PlaceOrder(order, 0);

            Assert.IsFalse(placeorderTest);
        }

        [Test]
        public void Given_Empty_Order_Object_PlaceOrder_Return_False_Results()
        {
            var iunitRepo = Substitute.For<IUnitOfWork>();
            var unitRep = new OrderService(iunitRepo);

            var placeorderTest = unitRep.PlaceOrder(new Order { },1);

            Assert.IsFalse(placeorderTest);
        }

        [Test]
        public void Check_If_GetOrders_Returns_Results()
        {
            var iunitRepo = Substitute.For<IUnitOfWork>();
            var unitRep = new OrderService(iunitRepo);

            var getorders = unitRep.GetOrders();

            Assert.IsNotNull(getorders);
        }

        [Test]
        public void Check_If_GetCustomers_Returns_Results()
        {
            var iunitRepo = Substitute.For<IUnitOfWork>();
            var unitRep = new OrderService(iunitRepo);

            var getcustomers = unitRep.GetCustomers();

            Assert.IsNotNull(getcustomers);
        }
    }
}

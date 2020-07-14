using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;
using Moq;
using AnyCompany.Models;
using AnyCompany.Repositories.CustomerRepository;
using NSubstitute;
using AnyCompany.Repositories.OrderRepository;
using NSubstitute.ExceptionExtensions;

namespace AnyCompany.Tests
{
    [TestFixture]
    public class OrderRepository
    {

        [Test]
        public void Given_Correct_CustomerId_Check_If_GetCustOrders_returns_Results()
        {
            var orderRepo = Substitute.For<IOrderRepository<Order>>();
            var custorders = orderRepo.GetCustomerOrders(1);

            Assert.IsNotNull(custorders);
        }

        [Test]
        public void Given_Correct_CustomerId_Check_If_GetAllOrders_returns_Results()
        {
            var orderRepo = Substitute.For<IOrderRepository<Order>>();
            var custorders = orderRepo.GetAllOrders();

            Assert.IsNotNull(custorders);
        }

        [Test]
        public void Given_Not_Correct_CustomerId_Check_If_GetAllOrders_returns_No_Results()
        {
            var orderRepo = Substitute.For<IOrderRepository<Order>>();
            var custorders = orderRepo.GetAllOrders();

            Assert.IsNull(custorders);
        }

        [Test]
        public void Given_Correct_OrderObject_Check_If_SaveOrder_Works()
        {
            var orderRepo = Substitute.For<IOrderRepository<Order>>();
            var order = new Order {
                Address = new Address{
                    HouseNumber = 23,
                    BuildingName = "Lister",
                    Country = "ZAR",
                    PostalCode = "2001",
                    CustomerId = 2,
                    Province = "Gauteng",
                    StreetName = "Jeppe",
                    Surburb = "MArshalltown"
                },
                Amount = 10,
                CustomerAccountNumber = "1244",
                CustomerId = 2,
                OrderId = 3,
                VAT = 0.2d
            };

            var response = orderRepo.Save(order);
            Assert.IsTrue(response);
        }

        //[Test]
        //public void Given_Incomplete_OrderObject_Check_If_SaveOrder_Returns_Null_Or_Throws_ArgumentException()
        //{
        //    var orderRepo = Substitute.For<IUnitOfWork>();
        //    var order = new Order
        //    {
        //    };

        //    Assert.Throws<ArgumentException>(() => orderRepo.Save(order));
        //}
    }
}

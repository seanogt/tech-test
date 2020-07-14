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
    }
}

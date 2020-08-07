using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using AnyCompany;
using AnyCompany.BUL.Services;
using AnyCompany.DAL;
using AnyCompany.DAL.Repositories;
using InvestecUnitTestsTests.MockData;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace InvestecUnitTests
{
    [TestFixture]
    public class OrderRepositoryTests : TestsBase<IOrderRepository>
    {
        [TestCase]
        public void GIVEN_VALID_ORDER_RETURN_TRUE()
        {
            //Arrange

            //Act
            OrderRepositoryWrapper orderWrapper = new OrderRepositoryWrapper();
            bool isSaved = orderWrapper.Save(ValidOrder);

            //Assert
            Assert.True(isSaved == true);
        }


        [TestCase]
        public void GIVEN_INVALID_ORDER_RETURN_FALSE()
        {
            //Arrange

            //Act
            OrderRepositoryWrapper orderWrapper = new OrderRepositoryWrapper();
            bool isSaved = orderWrapper.Save(InValidOrder);

            //Assert
            Assert.True(isSaved == false);
        }
    }
}

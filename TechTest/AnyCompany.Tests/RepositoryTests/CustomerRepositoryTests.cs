using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity.Migrations;
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
    public class CustomerRepositoryTests : TestsBase<ICustomerRepository>
    {
        [TestCase]
        public void GIVEN_VALID_CUSTOMERID_RETURN_CUSTOMER()
        {
            //Arrange
            Customer returnedCustomer = null;
            int customerIdToGet = 1;

            Customers.ToList().ForEach(i => RepoContext.Customers.AddOrUpdate(i));
            RepoContext.SaveChanges();

            //Act
            CustomerRepositoryWrapper CustomerWrapper = new CustomerRepositoryWrapper();
            returnedCustomer = CustomerWrapper.Get(customerIdToGet);

            //Assert
            Assert.True(returnedCustomer != null);
            Assert.True(returnedCustomer.CustomerId == customerIdToGet);
        }


        [TestCase]
        public void GIVEN_NONEXISTENT_CUSTOMERID_RETURN_NULL()
        {
            //Arrange
            Customer returnedCustomer = null;
            int customerIdToGet = -9999;

            Customers.ToList().ForEach(i => RepoContext.Customers.AddOrUpdate(i));
            RepoContext.SaveChanges();

            //Act
            CustomerRepositoryWrapper CustomerWrapper = new CustomerRepositoryWrapper();
            returnedCustomer = CustomerWrapper.Get(customerIdToGet);

            //Assert
            Assert.True(returnedCustomer == null);
        }

        [TestCase]
        public void RETURN_CUSTOMERS_WITH_LINKED_ORDERS_BY_DEFAULT()
        {
            //Arrange
            CustomersWithOrders.ToList().ForEach(i => RepoContext.Customers.AddOrUpdate(i));
            RepoContext.SaveChanges();

            //Act
            CustomerRepositoryWrapper customerWrapper = new CustomerRepositoryWrapper();
            IEnumerable<Customer> returnedCustomers = customerWrapper.GetAll();
            int orderCount = 0;
            returnedCustomers.ToList().ForEach(customer => orderCount += customer.Orders.Count);

            //Assert
            Assert.True(orderCount > 0);
            Assert.True(returnedCustomers.Count() > 0);
        }

        [TestCase]
        public void GIVEN_FALSE_ARGUMENT_RETURN_CUSTOMERS_WITHOUT_LINKED_ORDERS()
        {
            //Arrange
            CustomersWithOrders.ToList().ForEach(i => RepoContext.Customers.AddOrUpdate(i));
            RepoContext.SaveChanges();

            //Act
            CustomerRepositoryWrapper customerWrapper = new CustomerRepositoryWrapper();
            IEnumerable<Customer> returnedCustomers = customerWrapper.GetAll(false);
            int orderCount = 0;
            returnedCustomers.ToList().ForEach(customer => orderCount += customer.Orders.Count);

            //Assert
            Assert.True(orderCount == 0);
            Assert.True(returnedCustomers.Count() > 0);
        }
    }
}

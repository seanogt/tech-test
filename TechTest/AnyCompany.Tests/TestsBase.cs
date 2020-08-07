//using Microsoft.EntityFrameworkCore;

using AnyCompany;
using InvestecUnitTestsTests.MockData;
using AnyCompany.DAL;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace InvestecUnitTests
{
    public class TestsBase<T>
    {
        public RepoAnyCompanyContext RepoContext = RepositoryDataContext();
        public List<Customer> Customers = new List<Customer>
            {
                new Customer
                {
                    Name = "Mike America", Country = "US", DateOfBirth = DateTime.Parse("1987-06-01"), CustomerId = 1
                },
                       new Customer
                {
                    Name = "Jonathan Canada", Country = "CN", DateOfBirth = DateTime.Parse("1925-06-01"), CustomerId = 2
                },
                              new Customer
                {
                    Name = "FROM THE UK CHINKU", Country = "UK", DateOfBirth = DateTime.Parse("2001-06-01"), CustomerId = 3
                }
                              ,
                              new Customer
                {
                    Name = "Not in Database Adams", Country = "AG", DateOfBirth = DateTime.Parse("1982-12-12"), CustomerId = 50
                }
                                                ,
                              new Customer
                {
                    Name = "Malawi James", Country = "MW", DateOfBirth = DateTime.Parse("1989-02-06"), CustomerId = 4
                }
            };
        public List<Customer> CustomersWithOrders = new List<Customer>
            {
                new Customer
                {
                    Name = "I Have 1 Order", Country = "NM", DateOfBirth = DateTime.Parse("1987-06-01"), CustomerId = 12
                    , Orders = new List<Order>()
                    {
                        new Order
                        {
                            Amount = 500,
                            OrderId = 12,
                            CustomerId = 12,
                            VAT = 15
                        }
                    }
                },
                       new Customer
                {
                    Name = "I Have 2 Orders", Country = "ZW", DateOfBirth = DateTime.Parse("1925-06-01"), CustomerId = 13
                           ,            Orders = new List<Order>()
                    {
                        new Order
                        {
                            Amount = 500,
                            OrderId = 12,
                            CustomerId = 13,
                            VAT = 15
                        }

                                ,
                        new Order
                        {
                            Amount = -650,
                            OrderId = 13,
                            CustomerId = 13,
                            VAT = 5
                        }
                        }

                }
            };
        public Order ValidOrder = new Order
        {
            Amount = 8700,
            CustomerId = 1,
            VAT = 2,
        };

        public Order InValidOrder = new Order
        {
            Amount = -10,
            CustomerId = 2502,
            VAT = 3,
        };
        private static RepoAnyCompanyContext RepositoryDataContext()
        {
            return new RepoAnyCompanyContext(new DbContextOptionsBuilder()
                                    .UseSqlite("DataSource:inMemory:")
                                      .Options);
        }

        [SetUp]
        public void Setup()
        {

        }

        [TearDown]
        protected void DisposeDatabase()
        {
           // RepoContext.Dispose();
        }
    }
}
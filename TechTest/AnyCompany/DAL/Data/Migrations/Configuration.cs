namespace AnyCompany.Migrations
{
    using AnyCompany.DAL;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AnyCompanyContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AnyCompanyContext context)
        {
            List<Customer> dummyCustomersWithOrders = new List<Customer>
            {
                new Customer
                {
                    Name = "I Have 1 Order", Country = "NM", DateOfBirth = DateTime.Parse("1987-06-01")
                    , Orders = new List<Order>()
                    {
                        new Order
                        {
                            Amount = 500,
                            VAT = 1
                        }
                    }
                },
                new Customer
                {
                    Name = "I have zero Orders", Country = "ZA", DateOfBirth = DateTime.Parse("1985-09-11")
                    , Orders = new List<Order>()
                    {
                        new Order
                        {
                            Amount = 450,
                            VAT = 12
                        }
                    }
                },
                new Customer
                {
                    Name = "I Have 2 Orders", Country = "ZW", DateOfBirth = DateTime.Parse("1925-06-01")
                           ,            Orders = new List<Order>()
                    {
                        new Order
                        {
                            Amount = 6799,
                            VAT = 4
                        }

                                ,
                        new Order
                        {
                            Amount = 650,
                            VAT = 5
                        }
                        }
                }
            };

            context.Customers.AddRange(dummyCustomersWithOrders);
            context.SaveChanges();
        }
    }
}

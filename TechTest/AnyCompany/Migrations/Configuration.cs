namespace AnyCompany.Migrations
{
    using AnyCompany.DataAccessLayer;
    using AnyCompany.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AnyCompany.DataAccessLayer.CompanyContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CompanyContext context)
        {
            var customers = new List<Customer>
            {
                //Create some dummy customers 
                new Customer { Id = 1, Name = "Geja", Surname = "Khoza", Email = "gejakhoza@gmail.com",
                    Country = "South Africa", DateOfBirth = DateTime.Parse("1980-09-06") },
                new Customer { Id = 2, Name = "Ndo", Surname = "Mlondo", Email = "ndomlondo@gmail.com",
                    Country = "South Africa", DateOfBirth = DateTime.Parse("1984-06-01") },
                new Customer { Id = 3, Name = "Khehla", Surname = "Ceza", Email = "khahlaceza@gmail.com",
                    Country = "South Africa", DateOfBirth = DateTime.Parse("1986-04-09") }
            };

            customers.ForEach(c => context.Customers.Add(c));
            context.SaveChanges();
        }
    }
}

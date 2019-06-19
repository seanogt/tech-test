namespace AnyCompany.Migrations.ContextCustomer
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class ConfigurationCustomer : DbMigrationsConfiguration<AnyCompany.Entities.CustomerDbContext>
    {
        public ConfigurationCustomer()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\ContextCustomer";
        }

        protected override void Seed(AnyCompany.Entities.CustomerDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            context.Customers.AddOrUpdate(x => x.CustomerId,
            new Customer { Name = "Test1", DateOfBirth = Convert.ToDateTime("01/01/2000"), Country = "UK" },
            new Customer { Name = "Test2", DateOfBirth = Convert.ToDateTime("01/01/2000"), Country = "GB" },
            new Customer { Name = "Test3", DateOfBirth = Convert.ToDateTime("01/01/2000"), Country = "EU" });
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}

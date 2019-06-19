namespace AnyCompany.Migrations.ContextOrder
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class ConfigurationOrder : DbMigrationsConfiguration<AnyCompany.Entities.OrderDbContext>
    {
        public ConfigurationOrder()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\ContextOrder";
        }

        protected override void Seed(AnyCompany.Entities.OrderDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}

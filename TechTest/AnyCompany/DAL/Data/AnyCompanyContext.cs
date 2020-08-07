using System;
using System.Configuration;
using System.Data.Entity;
namespace AnyCompany.DAL
{
    public class AnyCompanyContext : DbContext
    {
        public AnyCompanyContext() : base(@ConfigurationManager.ConnectionStrings["AnyCompanyConnection"]?.ConnectionString)
        {
        }
        public DbSet<Order> Orders { get; set; }
		public DbSet<Customer> Customers { get; set; }
    }
}
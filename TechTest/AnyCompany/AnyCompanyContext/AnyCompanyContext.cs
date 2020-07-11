using AnyCompany.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.AnyCompanyContext
{
    public class AnyCompanyContext : DbContext, IAnyCompanyContext
    {

        public AnyCompanyContext(string connectionString) : base(GetOptions(connectionString))
        {
        }

        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }

        //If The project was based on .NetCore I would have used appsettings.json and configure
        //Based on the environment
        protected override void OnConfiguring(DbContextOptionsBuilder optionsbuilder)
        {
            optionsbuilder
                .UseSqlServer(@"Data Source=(local);Database=Orders;User Id=admin;Password=password;");
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Address> Addresses { get; set; }
    }
}

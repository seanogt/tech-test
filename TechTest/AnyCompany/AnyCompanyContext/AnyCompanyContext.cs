using AnyCompany.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.AnyCompanyContext
{
    public class CompanyContext : DbContext, IAnyCompanyContext
    {

        //If The project was based on .NetCore I would have used appsettings.json and configure
        //Based on the environment
        protected override void OnConfiguring(DbContextOptionsBuilder optionsbuilder)
        {
            optionsbuilder
                .UseSqlServer(@"Data Source=LAPTOP-6NNFSG9S\SQLEXPRESS;Database=Orders;User Id=username;Password=password;");
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Address> Addresses { get; set; }
    }
}

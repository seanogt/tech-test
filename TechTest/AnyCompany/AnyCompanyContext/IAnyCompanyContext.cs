using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnyCompany.Models;

namespace AnyCompany.AnyCompanyContext
{
    public interface IAnyCompanyContext
    {
        DbSet<Customer> Customers { get; set; }

        DbSet<Order> Orders { get; set; }

        DbSet<Address> Addresses { get; set; }
    }
}

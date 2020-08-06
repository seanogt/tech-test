using AnyCompany.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.DataAccessLayer
{
    public interface ICompanyContext
    {
        DbSet<Order> Orders { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<CustomerOrder> CustomerOrders { get; set; }
    }
}

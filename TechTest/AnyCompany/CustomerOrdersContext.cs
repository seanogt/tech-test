using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany
{
    public class CustomerOrdersContext : DbContext
    {
        public CustomerOrdersContext() : base("name=OrdersContext")
        {
            
        }
   
        public System.Data.Entity.DbSet<Order> Orders { get; set; }
        public System.Data.Entity.DbSet<Customer> Customers { get; set; }
    }
}

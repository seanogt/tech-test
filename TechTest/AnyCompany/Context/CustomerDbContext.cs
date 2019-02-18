using AnyCompany.Entity;
using System.Data.Entity;

namespace AnyCompany.Context
{
    public class CustomerDbContext : DbContext
    {
        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<VAT> Vats { get; set; }
    }
}

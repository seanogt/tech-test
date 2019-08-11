using System.Data.Entity;
using AnyCompany.Domain;

namespace AnyCompany.Repository
{
    public class OrdersDbContext : DbContext
    {
        public OrdersDbContext() : base("name = Orders")
        {
        }
        
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasRequired(p => p.Customer)
                .WithMany()
                .HasForeignKey(p => p.CustomersId)
                .WillCascadeOnDelete(false);
        }
    }
}

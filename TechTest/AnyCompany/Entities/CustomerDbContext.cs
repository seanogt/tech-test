using System.Data.Entity;

namespace AnyCompany.Entities
{
    public class CustomerDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public CustomerDbContext() : base("CustomerEntities")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
    }
}

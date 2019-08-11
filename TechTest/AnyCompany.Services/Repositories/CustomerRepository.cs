using System.Linq;
using AnyCompany.Domain;
using AnyCompany.Repository;

namespace AnyCompany.Services.Repositories
{
    public static class CustomerRepository
    {
        private static OrdersDbContext OrdersDbContext => new OrdersDbContext();

        public static Customer Load(int customerId)
        {
            return OrdersDbContext.Customers?.FirstOrDefault(x => x.CustomerId == customerId);
        }
    }
}

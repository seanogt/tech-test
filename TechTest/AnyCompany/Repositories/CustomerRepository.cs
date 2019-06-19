using AnyCompany.Entities;
using System.Collections.Generic;
using System.Linq;

namespace AnyCompany
{
    public static class CustomerRepository
    {
        public static Customer Load(int customerId)
        {
            using (var db = new CustomerDbContext())
            {
                var customer = db.Customers.Where(c => c.CustomerId == customerId).FirstOrDefault();
                return customer;
            }
        }

        public static List<Customer> LoadAll()
        {
            using (var db = new CustomerDbContext())
            {
                return db.Customers.ToList();
            }
        }
    }
}

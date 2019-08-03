using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnyCompany.Service.Facades;
using AnyCompany.Service.Models;

namespace AnyCompany.Service.DAL.DataManagers
{
    public class CustomersDbManager : ICustomerFacade
    {
        private readonly IDatabaseWrapper database;

        public CustomersDbManager(IDatabaseWrapper database)
        {
            this.database = database;
        }
        
        public async Task<Customer> GetCustomerById(string customerId)
        {
            var results = await this.database.ExecuteSqlFile("Customer/get-customer-by-id",
                new Dictionary<string, object>() {{"@customer_id", customerId}});

            var row = results.First();
            return new Customer(row["customer_id"].ToString(),
                row["country"].ToString(),
                DateTime.Parse(row["date_of_birth"].ToString()),
                row["name"].ToString());
        }

        public async Task<string> CreateCustomer(Customer customer)
        {
            var results = await this.database.ExecuteSqlFile("Customer/create-customer", new Dictionary<string, object>
            {
                {"@customer_id", customer.CustomerId},
                {"@country", customer.Country},
                {"@date_of_birth", customer.DateOfBirth},
                {"@name", customer.Name}
            });
            
            if (!results.Any())
            {
                // How did this happen? Index collision? Investigate
                throw new Exception("Order Creation - inv.");
            }

            return results.First()["_insertedIds"].ToString();
        }
    }
}
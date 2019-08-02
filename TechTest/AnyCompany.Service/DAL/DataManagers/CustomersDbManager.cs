using System;
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
            var results = await this.database.ExecuteSqlFile("Customer/get-customer-by-id", new [] { customerId });
            if (!results.Any())
            {
                // Raise not found
                throw new Exception("Not Found");
            }

            var row = results.First();
            return new Customer(row["country"].ToString(), DateTime.Parse(row["date_of_birth"].ToString()), row["name"].ToString());
        }

        public async Task<string> CreateCustomer(Customer customer)
        {
            var results = await this.database.ExecuteSqlFile("Customer/create-customer", new object [] {customer.Country, customer.DateOfBirth, customer.Name});
            if (!results.Any())
            {
                //How did this happeN? Raise an error?
            }

            return results.First()["_insertedIds"].ToString();
        }
    }
}
using System;
using System.Data.SqlClient;
using AnyCompany.Service.Models;

namespace AnyCompany.Service
{
    /// <summary>
    /// Legacy implementation of the repo, consume it if new one breaks.
    /// </summary>
    public static class CustomerRepository
    {
        // Prefer to use ENV variable for storing connection strings, instead of the repo.
        private static string ConnectionString = @"Data Source=(local);Database=Customers;User Id=admin;Password=password;";

        [Obsolete]
        public static Customer Load(int customerId)
        {

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
                
            // Keeping for the tasks sake, but this is a SQL injection vulnerability. This code is not used anymore.
            
            SqlCommand command = new SqlCommand("SELECT * FROM Customer WHERE CustomerId = " + customerId,
                connection);
            var reader = command.ExecuteReader();
            Customer customer = null;

            while (reader.Read())
            {
                customer = new Customer(
                    reader["customer_id"].ToString(),
                    reader["name"].ToString(),
                    DateTime.Parse(reader["date_of_birth"].ToString()),
                    reader["country"].ToString());
            }

            connection.Close();

            return customer;
        }
    }
}

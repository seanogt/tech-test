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
        private static string ConnectionString = @"Data Source=(local);Database=Customers;User Id=admin;Password=password;";

        public static Customer Load(int customerId)
        {

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
                
            // Keeping for the tasks sake, but this is a SQL injection vulnerability.
            SqlCommand command = new SqlCommand("SELECT * FROM Customer WHERE CustomerId = " + customerId,
                connection);
            var reader = command.ExecuteReader();
            Customer customer = null;

            while (reader.Read())
            {
                customer = new Customer(
                    reader["Name"].ToString(),
                    DateTime.Parse(reader["DateOfBirth"].ToString()),
                    reader["Country"].ToString());
            }

            connection.Close();

            return customer;
        }
    }
}

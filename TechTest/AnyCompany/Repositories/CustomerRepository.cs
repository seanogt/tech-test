using AnyCompany.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AnyCompany.Repositories
{
    /// <summary>
    /// This is the Customer Repository, responsible for managing customer persistance and retrieval.
    /// </summary>
    public static class CustomerRepository
    {
        /// <summary>
        /// This loads a Customer object.
        /// </summary>
        /// <param name="customerId">The Id of the customer this order is placed agains.</param>
        public static Customer Load(int customerId)
        {
            Customer customer = new Customer();

            SqlConnection connection = new SqlConnection(Properties.Settings.Default.CustomerConnectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM Customer WHERE CustomerId = @CustomerId", connection);

            command.Parameters.AddWithValue("@CustomerId", customerId);

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                customer.CustomerId = int.Parse(reader["CustomerId"].ToString());
                customer.Name = reader["Name"].ToString();
                customer.DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString());
                customer.Country = reader["Country"].ToString();
            }

            connection.Close();

            return customer;
        }

        public static List<Customer> GetAll()
        {
            List<Customer> customers = new List<Customer>();

            SqlConnection connection = new SqlConnection(Properties.Settings.Default.CustomerConnectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM Customer", connection);

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                customers.Add(new Customer()
                {
                    CustomerId = int.Parse(reader["CustomerId"].ToString()),
                    Name = reader["Name"].ToString(),
                    DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString()),
                    Country = reader["Country"].ToString()
                });
            }

            connection.Close();

            return customers;
        }
    }
}

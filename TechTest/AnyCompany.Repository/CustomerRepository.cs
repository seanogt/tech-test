using AnyCompany.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AnyCompany.Repository
{
    public static class CustomerRepository
    {
        private static string _connectionString = ConfigurationManager.ConnectionStrings["AnyCompany"].ConnectionString;

        public static Customer Save(Customer customer)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {

                SqlCommand command = new SqlCommand("INSERT INTO Customer (Name, DateOfBirth, Country) OUTPUT INSERTED.CustomerId VALUES (@Name, @DateOfBirth, @Country) ", connection);

                command.Parameters.AddWithValue("@Name", customer.Name);
                command.Parameters.AddWithValue("@DateOfBirth", customer.DateOfBirth);
                command.Parameters.AddWithValue("@Country", customer.Country);

                connection.Open();

                customer.CustomerId = (int)command.ExecuteScalar();

            }

            return customer;
        }

        public static Customer Load(int customerId)
        {
            Customer customer = new Customer();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Customer WHERE CustomerId = " + customerId, connection);

                connection.Open();

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    customer.CustomerId = Int32.Parse(reader["CustomerId"].ToString());
                    customer.Name = reader["Name"].ToString();
                    customer.DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString());
                    customer.Country = reader["Country"].ToString();
                }

                reader.Close();

            }

            return customer;
        }

        public static List<Customer> LoadCollection()
        {
            List<Customer> customers = new List<Customer>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Customer", connection);

                connection.Open();

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Customer customer = new Customer()
                    {
                        CustomerId = Int32.Parse(reader["CustomerId"].ToString()),
                        Name = reader["Name"].ToString(),
                        DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString()),
                        Country = reader["Country"].ToString()
                    };

                    customers.Add(customer);
                }

                reader.Close();
            }

            return customers;
        }
    }
}

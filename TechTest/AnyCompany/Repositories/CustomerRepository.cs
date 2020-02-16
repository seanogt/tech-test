using System;
using System.Data.SqlClient;
using AnyCompany.Models;

namespace AnyCompany.Repositories
{
    public static class CustomerRepository
    {
        private static string ConnectionString = @"Data Source=(local);Database=Customers;User Id=admin;Password=password;";

        public static Customer Get(int customerId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand(
                    "SELECT * FROM Customer WHERE CustomerId = " + customerId,
                    connection
                );

                var reader = command.ExecuteReader();

                var customer = new Customer();

                while (reader.Read())
                {
                    customer.Name = reader["Name"].ToString();
                    customer.DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString());
                    customer.Country = reader["Country"].ToString();
                }

                connection.Close();

                return customer;
            }
        }

        public static void Add(Customer customer)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqlCommand("INSERT INTO Customer VALUES (@Name, @DateOfBirth, @Country)", connection);

                command.Parameters.AddWithValue("@OrderId", customer.Name);
                command.Parameters.AddWithValue("@Amount", customer.DateOfBirth);
                command.Parameters.AddWithValue("@VAT", customer.Country);

                command.ExecuteNonQuery();
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AnyCompany
{
    public static class CustomerRepository
    {
        public static string ConnectionString;

        public static Customer Load(int customerId)
        {
            Customer customer = new Customer();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = new SqlCommand("SELECT * FROM Customer WHERE CustomerId = @CustomerId", connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@CustomerId", customerId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customer = ExtractCustomer(reader);
                    }
                }

                connection.Close();
            }

            return customer;
        }

        public static IEnumerable<Customer> LoadAll()
        {
            List<Customer> customers = new List<Customer>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = new SqlCommand("SELECT * FROM Customer", connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var customer = ExtractCustomer(reader);
                        customers.Add(customer);
                    }
                }
                connection.Close();
            }
            return customers;
        }

        private static Customer ExtractCustomer(SqlDataReader reader)
        {
            Customer customer = new Customer
            {
                CustomerId = (int)reader["CustomerId"],
                Name = reader["Name"].ToString(),
                DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString()),
                Country = reader["Country"].ToString()
            };

            return customer;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace AnyCompany
{
    public static class CustomerRepository
    {
        private static string ConnectionString = @"Data Source=(local);Database=Customers;User Id=admin;Password=password;";

        public static Customer Load(int customerId)
        {
            Customer customer = new Customer();

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM Customer WHERE  = " + customerId,
                connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                customer.CustomerId = Convert.ToInt32(reader["CustomerId"].ToString());
                customer.Name = reader["Name"].ToString();
                customer.DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString());
                customer.Country = reader["Country"].ToString();
            }

            connection.Close();

            return customer;
        }

        //DRS - Added
        //Load all customers
        public static List<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Customer", connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Customer customer = new Customer()
                    {
                        CustomerId = Convert.ToInt32(reader["CustomerId"].ToString()),
                        Name = reader["Name"].ToString(),
                        DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString()),
                        Country = reader["Country"].ToString()
                    };
                    customers.Add(customer);
                }
            }
            return customers;
        }
    }
}

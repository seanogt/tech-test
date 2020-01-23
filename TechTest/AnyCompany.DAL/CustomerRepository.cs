using AnyCompany.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace AnyCompany.DAL
{
    public static class CustomerRepository
    {
      
        private static string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString; //@"Data Source=(local);Database=Customers;User Id=admin;Password=password;";

        public static string ConnectionStr
        {
            get; set;
        }
        public static Customer Load(int customerId)
        {
            // Use of dapper here will make it better.
            Customer customer = new Customer();
            using (SqlConnection connection = new SqlConnection(ConnectionStr))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Customer WHERE CustomerId = " + customerId, connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    customer.Name = reader["Name"].ToString();
                    customer.DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString());
                    customer.Country = reader["Country"].ToString();
                }
            }
            return customer;
        }
        public static IEnumerable<Customer> LoadAll()
        {
            // Use of dapper here will make it better.
            List<Customer> customers = new List<Customer>();
            using (SqlConnection connection = new SqlConnection(ConnectionStr))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Customer", connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Customer customer = new Customer()
                    {
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
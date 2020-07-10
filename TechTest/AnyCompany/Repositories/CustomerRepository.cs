﻿using System;
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

            SqlCommand command = new SqlCommand("SELECT * FROM Customer WHERE CustomerId = " + customerId,
                connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                customer.Name = reader["Name"].ToString();
                customer.DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString());
                customer.Country = reader["Country"].ToString();
            }

            connection.Close();

            return customer;
        }

        public static int Save(Customer customer)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("INSERT INTO Customer VALUES (@CutomerId, @Name, @DateOfBirth, @Country)", connection);

            command.Parameters.AddWithValue("@CutomerId", customer.CustomerId);
            command.Parameters.AddWithValue("@Name", customer.Name);
            command.Parameters.AddWithValue("@DateOfBirth", customer.DateOfBirth);
            command.Parameters.AddWithValue("@Country", customer.Country);

            var result = command.ExecuteNonQuery();

            connection.Close();
            return result;
        }

        public static List<Customer> GetCustomers() {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM Customer",
                connection);
            var reader = command.ExecuteReader();

            List<Customer> customers = new List<Customer>();
            while (reader.Read())
            {
                Customer customer = new Customer();

                customer.CustomerId = Convert.ToInt32(reader["CustomerId"]);
                customer.Name = reader["Name"].ToString();
                customer.DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString());
                customer.Country = reader["Country"].ToString();

                customers.Add(customer);
            }

            connection.Close();
            return customers;
        }
    }
}

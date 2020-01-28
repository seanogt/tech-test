﻿using AnyCompany.Models;
using System;
using System.Data.SqlClient;

namespace AnyCompany.Repositories
{
    public static class CustomerRepository
    {
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
                customer.Name = reader["Name"].ToString();
                customer.DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString());
                customer.Country = reader["Country"].ToString();
            }

            connection.Close();

            return customer;
        }
    }
}

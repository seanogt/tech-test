// <copyright file="CustomerRepository.cs" company="Investec Bank">
// Copyright © Investec Bank 2018
// </copyright>

namespace AnyCompany.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    /// <summary>
    /// This static class has been explicitly requested to remain in place. It provides the core functionality for
    /// <see cref="CustomerRepositoryInstance"/>.
    /// </summary>
    internal static class CustomerRepository
    {
        /// <summary>
        /// The connection string to use for connecting to the database.
        /// </summary>
        // TODO: The connection string should be built from injected config elements, not set statically in code.
        private static readonly string ConnectionString =
            @"Data Source=(local);Database=Customers;User Id=admin;Password=password;";

        /// <summary>
        /// Loads a cutomer by it's customerId.
        /// </summary>
        /// <param name="customerId">The id of the customer to load.</param>
        /// <returns>The customer with the given id, if one exists.</returns>
        public static Customer Load(int customerId)
        {
            Customer customer = new Customer();

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            SqlCommand command = new SqlCommand(
                "SELECT CustomerId, Name, DateOfBirth, Country FROM Customer WHERE CustomerId = " + customerId,
                connection);
            var reader = command.ExecuteReader();

            if (!reader.HasRows)
            {
                throw new ApplicationException($"Customer not found with id: {customerId}");
            }

            reader.Read();

            customer.CustomerId = int.Parse(reader["CustomerId"].ToString());
            customer.Name = reader["Name"].ToString();
            customer.DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString());
            customer.Country = reader["Country"].ToString();

            if (reader.Read())
            {
                throw new ApplicationException($"More than one customer found with id: {customerId}");
            }

            connection.Close();

            return customer;
        }

        /// <summary>
        /// Loads all customers.
        /// </summary>
        /// <returns>The set of all customers.</returns>
        public static IEnumerable<Customer> LoadAll()
        {
            var customers = new List<Customer>();

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            SqlCommand command =
                new SqlCommand("SELECT CustomerId, Name, DateOfBirth, Country  FROM Customer", connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var customer = new Customer
                {
                    CustomerId = int.Parse(reader["CustomerId"].ToString()),
                    Name = reader["Name"].ToString(),
                    DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString()),
                    Country = reader["Country"].ToString(),
                };
                customers.Add(customer);
            }

            connection.Close();

            return customers;
        }
    }
}

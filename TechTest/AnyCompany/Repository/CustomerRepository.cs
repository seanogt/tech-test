// <copyright file="CustomerRepository.cs" company="Investec Bank">
// Copyright © Investec Bank 2018
// </copyright>

namespace AnyCompany.Repository
{
    using System;
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
                "SELECT * FROM Customer WHERE CustomerId = " + customerId,
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
    }
}

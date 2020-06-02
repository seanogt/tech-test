using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using AnyCompany.Models;

namespace AnyCompany.DataRepositories
{
    public static class CustomerRepository
    {
        //Hardcoding connection strings is not a good idea. This will be better suited in a settings / config file
        //As this is a class library, we'll skip the idea of moving it into settings, rather, the better idea will be to have the consuming app provide any connection strings necessary
        private static string ConnectionString = @"Data Source=(local);Database=Customers;User Id=admin;Password=password;";        

        /// <summary>
        /// Get a customer given a customer ID
        /// </summary>
        /// <param name="customerId">Customer ID to retrieve</param>
        /// <returns>Customer Model</returns>
        public static CustomerModel Load(int customerId)
        {
            CustomerModel customer = new CustomerModel();

            try
            {
                using (SqlConnection connection = GetSqlConnection())
                {
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
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Repository error: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Gets a list of all the customers existing in the provided database
        /// </summary>
        /// <returns></returns>
        public static List<CustomerModel> GetAllCustomers()
        {            
            try
            {
                List<CustomerModel> returnList = new List<CustomerModel>();
                using (SqlConnection connection = GetSqlConnection())
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("SELECT * FROM Customer",
                        connection);
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    { 
                        var customer = new CustomerModel();
                        customer.CustomerId = int.Parse(reader["customerId"].ToString());
                        customer.Name = reader["Name"].ToString();
                        customer.DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString());
                        customer.Country = reader["Country"].ToString();
                        returnList.Add(customer);
                    }

                    connection.Close();

                    return returnList;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Repository error: {ex.Message}");
                return null;
            }
        }

        #region Helpers

        /// <summary>
        /// Helper method to get a relevant connection string. Decouples/seperates concern from the methods using the connection.
        /// </summary>
        /// <returns></returns>
        private static SqlConnection GetSqlConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        #endregion
    }
}

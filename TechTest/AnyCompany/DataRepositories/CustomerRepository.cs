using System;
using System.Data.SqlClient;
using AnyCompany.Models;

namespace AnyCompany.DataRepositories
{
    public static class CustomerRepository
    {
        //Hardcoding connection strings is not a good idea. This will be better suited in a settings / config file
        //As this is a class library, we'll skip the idea of moving it into settings, rather, the better idea will be to have the consuming app provide any connection strings necessary
        private static string ConnectionString = @"Data Source=(local);Database=Customers;User Id=admin;Password=password;";


        public static CustomerModel Load(int customerId)
        {
            CustomerModel customer = new CustomerModel();

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
    }
}

using AnyCompany.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AnyCompany.DAL
{
    public static class CustomerRepository
    {
        public static Customer Load(int customerId, SqlConnection connection)
        {
            Customer customer = new Customer();

            SqlCommand command = new SqlCommand("SELECT * FROM Customer WHERE CustomerId = @CustomerId",
                connection);

            command.Parameters.AddWithValue("@CustomerId", customerId);

            var reader = command.ExecuteReader();

            //TODO: Implement Mapper
            while (reader.Read())
            {
                customer.Name = reader["Name"].ToString();
                customer.DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString());
                customer.CountryId = Convert.ToInt32(reader["CountryId"]);
            }

            return customer;
        }
    }
}

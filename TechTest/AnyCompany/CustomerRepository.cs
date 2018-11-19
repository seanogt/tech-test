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

            using (SqlConnection connection = new SqlConnection(ConnectionString))
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

                //linked Orders

                customer.LinkedOrders = new List<Order>();
                
                command = new SqlCommand("SELECT * FROM Orders WHERE CustomerId = " + customerId,
                    connection);
                reader = command.ExecuteReader();

                // assuming data is valid on Save(), hence not making use of tryParse(obj, out obj)
                while (reader.Read())
                {
                    customer.LinkedOrders.Add(new Order {
                        OrderId = int.Parse(reader["OrderId"].ToString()), 
                        VAT = float.Parse(reader["VAT"].ToString()),
                        Amount = double.Parse(reader["Amount"].ToString()),
                        CustomerId = int.Parse(reader["CustomerId"].ToString())
                    });
                }
            }

            return customer;
        }
    }
}

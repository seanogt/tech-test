using AnyCompany.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace AnyCompany.Repository
{
    public static class OrderRepository
    {
        private static string _connectionString = ConfigurationManager.ConnectionStrings["AnyCompany"].ConnectionString;

        public static Order Save(Order order)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("INSERT INTO Orders (CustomerId, Amount, VAT) OUTPUT INSERTED.OrderId VALUES (@CustomerId, @Amount, @VAT)", connection);

                command.Parameters.AddWithValue("@CustomerId", order.CustomerId);
                command.Parameters.AddWithValue("@Amount", order.Amount);
                command.Parameters.AddWithValue("@VAT", order.VAT);

                connection.Open();

                order.OrderId = (int)command.ExecuteScalar();
            }

            return order;
        }

        public static List<Order> LoadCollection(int customerId)
        {
            List<Order> orders = new List<Order>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Orders WHERE CustomerId = " + customerId, connection);

                connection.Open();

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Order order = new Order()
                    {
                        CustomerId = Int32.Parse(reader["CustomerId"].ToString()),
                        Amount = double.Parse(reader["Amount"].ToString()),
                        VAT = double.Parse(reader["VAT"].ToString())
                    };

                    orders.Add(order);
                }
            }
            return orders;
        }
    }
}

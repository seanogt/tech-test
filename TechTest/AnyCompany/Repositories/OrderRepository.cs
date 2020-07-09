using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AnyCompany
{
    internal static class OrderRepository
    {
        private static string ConnectionString = @"Data Source=(local);Database=Orders;User Id=admin;Password=password;";

        public static void Save(Order order)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("INSERT INTO Orders VALUES (@OrderId, @CustomerId, @Amount, @VAT)", connection);

            command.Parameters.AddWithValue("@OrderId", order.OrderId);
            command.Parameters.AddWithValue("@CustomerId", order.CustomerId);
            command.Parameters.AddWithValue("@Amount", order.Amount);
            command.Parameters.AddWithValue("@VAT", order.VAT);

            command.ExecuteNonQuery();

            connection.Close();
        }

        public static List<Order> GetOrders(int customerId)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM Order Where CustomerId = " + customerId, connection);
            var reader = command.ExecuteReader();

            List<Order> orders = new List<Order>();
            while (reader.Read())
            {
                Order order = new Order();
                order.Amount = Convert.ToDecimal(reader["Amount"]);
                order.VAT = Convert.ToDecimal(reader["VAT"]);
                orders.Add(order);
            }
            connection.Close();
            return orders;
        }
    }
}

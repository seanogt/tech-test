using System;
using System.Data.SqlClient;
using AnyCompany.Service.Models;

namespace AnyCompany.Service
{
    internal class OrderRepository
    {
        // Never store creds in the codebase.
        private static string ConnectionString = @"Data Source=(local);Database=Orders;User Id=admin;Password=password;";

        [Obsolete]
        public void Save(Order order)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("INSERT INTO Orders VALUES (@OrderId, @Amount, @VAT)", connection);

            command.Parameters.AddWithValue("@OrderId", order.OrderId);
            command.Parameters.AddWithValue("@Amount", order.Amount);
            command.Parameters.AddWithValue("@VAT", order.VAT);

            command.ExecuteNonQuery();

            connection.Close();
        }
    }
}

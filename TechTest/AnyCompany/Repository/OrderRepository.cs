// <copyright file="OrderRepository.cs" company="Investec Bank">
// Copyright © Investec Bank 2018
// </copyright>

namespace AnyCompany.Repository
{
    using System.Collections.Generic;
    using System.Data.SqlClient;

    /// <summary>
    /// Provides a SqlClient implementation of the <see cref="IOrderRepository"/> interface.
    /// </summary>
    internal class OrderRepository : IOrderRepository
    {
        // TODO: The connection string should be built from injected config elements, not set statically in code.
        private static readonly string ConnectionString =
            @"Data Source=(local);Database=Orders;User Id=admin;Password=password;";

        /// <inheritdoc/>
        public void Save(Order order)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            SqlCommand command =
                new SqlCommand("INSERT INTO Orders VALUES (@OrderId, @Amount, @VAT, @CustomerId)", connection);

            command.Parameters.AddWithValue("@OrderId", order.OrderId);
            command.Parameters.AddWithValue("@Amount", order.Amount);
            command.Parameters.AddWithValue("@VAT", order.VAT);
            command.Parameters.AddWithValue("@CustomerId", order.CustomerId);

            command.ExecuteNonQuery();

            connection.Close();
        }

        /// <inheritdoc/>
        public IEnumerable<Order> LoadAll()
        {
            var orders = new List<Order>();

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT OrderId, Amount, VAT, CustomerId FROM Orders", connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var order = new Order
                {
                    OrderId = int.Parse(reader["OrderId"].ToString()),
                    Amount = double.Parse(reader["Amount"].ToString()),
                    VAT = double.Parse(reader["VAT"].ToString()),
                    CustomerId = int.Parse(reader["CustomerId"].ToString()),
                };
                orders.Add(order);
            }

            connection.Close();

            return orders;
        }
    }
}

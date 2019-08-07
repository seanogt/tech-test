// <copyright file="OrderRepository.cs" company="Investec Bank">
// Copyright © Investec Bank 2018
// </copyright>

namespace AnyCompany.Repository
{
    using System.Data.SqlClient;

    /// <summary>
    /// Provides a SqlClient implementation of the <see cref="IOrderRepository"/> interface.
    /// </summary>
    internal class OrderRepository : IOrderRepository
    {
        private static readonly string ConnectionString = @"Data Source=(local);Database=Orders;User Id=admin;Password=password;";

        /// <summary>
        /// Saves a given order to the database.
        /// </summary>
        /// <param name="order">The order to save.</param>
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

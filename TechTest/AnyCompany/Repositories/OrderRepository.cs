using AnyCompany.Models;
using System.Data.SqlClient;

namespace AnyCompany.Repositories
{
    /// <summary>
    /// This is the OrderRepository Repository, responsible for managing customer persistance and retrieval.
    /// </summary>
    internal class OrderRepository
    {
        /// <summary>
        /// This stores an Order object.
        /// </summary>
        /// <param name="order">The order information.</param>
        public void Save(Order order)
        {
            SqlConnection connection = new SqlConnection(Properties.Settings.Default.OrderConnectionString);
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

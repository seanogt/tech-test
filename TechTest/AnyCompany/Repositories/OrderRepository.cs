using AnyCompany.Models;
using System.Collections.Generic;
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

            SqlCommand command = new SqlCommand("INSERT INTO Orders VALUES (@OrderId, @Amount, @VAT, @CustomerId)", connection);

            command.Parameters.AddWithValue("@OrderId", order.OrderId);
            command.Parameters.AddWithValue("@Amount", order.Amount);
            command.Parameters.AddWithValue("@VAT", order.VAT);
            command.Parameters.AddWithValue("@CustomerId", order.CustomerId);

            command.ExecuteNonQuery();

            connection.Close();
        }

        public List<Order> GetAllByCustomerId(int customerId)
        {
            List<Order> orders = new List<Order>();

            SqlConnection connection = new SqlConnection(Properties.Settings.Default.CustomerConnectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM Orders WHERE CustomerId = @CustomerId", connection);

            command.Parameters.AddWithValue("@CustomerId", customerId);

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                orders.Add(new Order()
                {
                    OrderId = int.Parse(reader["OrderId"].ToString()),
                    Amount = double.Parse(reader["Amount"].ToString()),
                    VAT = double.Parse(reader["VAT"].ToString()),
                    CustomerId = int.Parse(reader["CustomerId"].ToString())
                });
            }

            connection.Close();

            return orders;
        }
    }
}

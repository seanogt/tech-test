using AnyCompany.Repositories;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace AnyCompany
{
    internal class OrderRepository : IOrderRepository
    {
        private static string ConnectionString = @"Data Source=(local);Database=Orders;User Id=admin;Password=password;";

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


        public IEnumerable<Order> GetOrders()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Order> GetOrdersByCustomerId(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}

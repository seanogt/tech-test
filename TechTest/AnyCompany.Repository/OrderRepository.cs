using AnyCompany.Models;
using AnyCompany.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.Repository
{
    internal class OrderRepository : IOrderRepository, IDisposable
    {
        private static string ConnectionString = @"Data Source=(local);Database=Orders;User Id=admin;Password=password;";

        private readonly SqlConnection connection = null;

        public OrderRepository()
        {
            try
            {
                connection = new SqlConnection(ConnectionString);
                connection.Open();
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        public void Dispose()
        {
            if (connection != null)
                connection.Close();
        }

        public List<Order> GetOrders()
        {
            throw new NotImplementedException();
        }

        public void Save(Order order)
        {
            SqlCommand command = new SqlCommand("INSERT INTO Orders VALUES (@OrderId, @Amount, @VAT)", connection);

            command.Parameters.AddWithValue("@OrderId", order.OrderId);
            command.Parameters.AddWithValue("@Amount", order.Amount);
            command.Parameters.AddWithValue("@VAT", order.VAT);

            command.ExecuteNonQuery();

            connection.Close();
        }
    }
}

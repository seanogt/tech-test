using AnyCompany.Repositories;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace AnyCompany
{
    internal class OrderRepository : IOrderRepository
    {
        private static string ConnectionString = @"Data Source=(local);Database=Orders;User Id=admin;Password=password;";

        public bool Save(Order order)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("INSERT INTO Orders VALUES (@OrderId, @Amount, @VAT)", connection);

                    command.Parameters.AddWithValue("@OrderId", order.OrderId);
                    command.Parameters.AddWithValue("@Amount", order.Amount);
                    command.Parameters.AddWithValue("@VAT", order.VAT);

                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (System.Exception)
            {

                return false;
            }

            return true;
          
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

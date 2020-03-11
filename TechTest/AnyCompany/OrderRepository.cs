using System.Data;
using System.Data.SqlClient;

namespace AnyCompany
{
    internal class OrderRepository : IOrderRepository
    {
        private static string ConnectionString = @"Data Source=(local);Database=Orders;User Id=admin;Password=password;";

        public void Save(Order order, int customerId)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("INSERT INTO Orders VALUES (@OrderId,@CustomerId, @Amount, @VAT)", connection);

            command.Parameters.AddWithValue("@OrderId", order.OrderId);
            command.Parameters.AddWithValue("@CustomerId", customerId);
            command.Parameters.AddWithValue("@Amount", order.Amount);
            command.Parameters.AddWithValue("@VAT", order.VAT);

            command.ExecuteNonQuery();

            connection.Close();
        }

        public DataSet GellAllOrders()
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Orders)", connection);

            DataSet orders = new DataSet();
            adapter.Fill(orders, "AllOrders");

            connection.Close();

            return orders;

           
        }
    }
}

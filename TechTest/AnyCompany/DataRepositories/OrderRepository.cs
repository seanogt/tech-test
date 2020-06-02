using System.Data.SqlClient;
using AnyCompany.Models;

namespace AnyCompany.DataRepositories
{
    internal class OrderRepository
    {
        //Hardcoding connection strings is not a good idea. This will be better suited in a settings / config file
        //As this is a class library, we'll skip the idea of moving it into settings, rather, the better idea will be to have the consuming app provide any connection strings necessary
        private static string ConnectionString = @"Data Source=(local);Database=Orders;User Id=admin;Password=password;";

        public void Save(OrderModel order)
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

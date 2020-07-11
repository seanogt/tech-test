using System.Data.SqlClient;
using AnyCompany.Models;
using AnyCompany.AnyCompanyContext;

namespace AnyCompany.Repositories.OrderRepository
{
    internal class OrderRepository
    {
        private static string ConnectionString = @"Data Source=(local);Database=Orders;User Id=admin;Password=password;";

        private IAnyCompanyContext _anycompanycontext;

        public OrderRepository()
        {

        }

        public OrderRepository(IAnyCompanyContext context)
        {
            _anycompanycontext = context;
        }

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

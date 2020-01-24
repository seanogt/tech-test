using System.Data.SqlClient;

namespace AnyCompany
{
    public class OrderRepository
    {
        private static string ConnectionString = @"Data Source=DESKTOP-21TVJEC;Initial Catalog=AnyCompany;Integrated Security=True;Persist Security Info=False;Enlist=False;Pooling=True;Min Pool Size=1;Max Pool Size=100;Connect Timeout=15;User Instance=False";//ConfigurationSettings.AppSettings["AnyCompany"].ToString();// @"Data Source=(local);Database=Orders;User Id=admin;Password=password;";

        public void Save(Order order)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("INSERT INTO Orders VALUES (@OrderId, @Amount, @VAT,@CustomerId)", connection);

            command.Parameters.AddWithValue("@OrderId", order.OrderId);
            command.Parameters.AddWithValue("@Amount", order.Amount);
            command.Parameters.AddWithValue("@VAT", order.VAT);
            command.Parameters.AddWithValue("@CustomerId", order.CustomerId);

            command.ExecuteNonQuery();

            connection.Close();
        }
    }
}

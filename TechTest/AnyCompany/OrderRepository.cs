using System;
using System.Data.SqlClient;

namespace AnyCompany
{
    internal class OrderRepository
    {
        private static string ConnectionString = @"Data Source=(local);Database=AnyCompany;User Id=admin;Password=password1;";

        public int Save(Order order)
        {
            try
            {
                SqlConnection connection = new SqlConnection(ConnectionString);
                connection.Open();

                SqlCommand command = new SqlCommand("INSERT INTO Orders VALUES (@OrderId, @Amount, @CustomerId, @VAT)", connection);

                command.Parameters.AddWithValue("@OrderId", order.OrderId);
                command.Parameters.AddWithValue("@Amount", order.Amount);
                command.Parameters.AddWithValue("@CustomerId", Convert.ToInt32(order.Customer.CustomerId));
                command.Parameters.AddWithValue("@VAT", order.VAT);
                

                int result = command.ExecuteNonQuery();

                connection.Close();

                return result;
            }
            catch(Exception ex)
            {
                ex.ToString();

                return -1;
            }
        }
    }
}

using AnyCompany.Domain;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace AnyCompany.DAL
{
    public class OrderRepository
    {
        private static string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;  //@"Data Source=(local);Database=Orders;User Id=admin;Password=password;";
        public void Save(Order order)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("INSERT INTO Orders VALUES (@OrderId, @Amount, @VAT. @CustomerId)", connection);
                command.Parameters.AddWithValue("@OrderId", order.OrderId);
                command.Parameters.AddWithValue("@Amount", order.Amount);
                command.Parameters.AddWithValue("@VAT", order.VAT);
                command.Parameters.AddWithValue("@CustomerId", order.CustomerId);
                command.ExecuteNonQuery();
            }
        }
        public IEnumerable<Order> SelectAll()
        {
            List<Order> orders = new List<Order>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Orders", connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Order order = new Order()
                    {
                        OrderId = int.Parse(reader["OrderId"].ToString()),
                        Amount = double.Parse(reader["Amount"].ToString()),
                        CustomerId = int.Parse(reader["CustomerId"].ToString()),
                        VAT = double.Parse(reader["VAT"].ToString())
                    };
                    orders.Add(order);
                }
            }
            return orders;
        }
    }
}
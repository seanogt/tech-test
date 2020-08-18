using AnyCompany.Models;
using AnyCompany.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.Repository
{
    public class OrderRepository : IOrderRepository, IDisposable
    {
        private static string ConnectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

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
            List<Order> Orders = new List<Order>();

            SqlCommand command = new SqlCommand("spGetOrders",
                connection);

            command.CommandType = CommandType.StoredProcedure;

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                Order order = new Order();
                order.Customer = new Customer();

                order.OrderId = (int)reader["OrderId"];
                order.Amount = (double)reader["Amount"];
                order.VAT = (double)reader["Country"];
                order.CustomerId = (int)reader["CustomerId"];
                order.Customer.CustomerId = (int)reader["CustomerId"];
                order.Customer.Name = reader["Name"].ToString();
                order.Customer.DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString());
                order.Customer.Country = reader["Country"].ToString();

                Orders.Add(order);
            }

            return Orders;
        }

        public void Save(Order order)
        {
            SqlCommand command = new SqlCommand("spPlaceOrder", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Amount", order.Amount);
            command.Parameters.AddWithValue("@VAT", order.VAT);
            command.Parameters.AddWithValue("@CustomerId", order.CustomerId);

            command.ExecuteNonQuery();

            connection.Close();
        }
    }
}

using AnyCompany.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AnyCompany.DAL
{
    internal class OrderRepository : RepositoryBase, IOrderRepository
    {
        public OrderRepository(string connectionString)
            : base(connectionString)
        {

        }

        public bool Save(Order order)
        {
            SqlCommand command = new SqlCommand("INSERT INTO Orders VALUES (@OrderId, @Amount, @VAT, @CustomerId)");

            command.Parameters.AddWithValue("@OrderId", order.OrderId);
            command.Parameters.AddWithValue("@Amount", order.Amount);
            command.Parameters.AddWithValue("@VAT", order.VAT);
            command.Parameters.AddWithValue("@CustomerId", order.CustomerId);

            return ExecuteCommand(command);
        }

        public IEnumerable<Order> GetOrdersByCustomerId(int customerId)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Orders WHERE CustomerId = @CustomerId");
            command.Parameters.AddWithValue("@CustomerId", customerId);
            var reader = ExecuteQuery(command);

            List<Order> orders = new List<Order>();

            //TODO: Implement Mapper
            while (reader.Read())
            {
                orders.Add(new Order
                {
                    OrderId = Convert.ToInt32(reader["OrderId"]),
                    Amount = Convert.ToDouble(reader["Amount"]),
                    VAT = Convert.ToDouble(reader["VAT"]),
                    CustomerId = Convert.ToInt32(reader["CustomerId"])
                });
            }

            return orders;
        }
    }
}

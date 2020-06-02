using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using AnyCompany.Models;

namespace AnyCompany.DataRepositories
{
    internal class OrderRepository
    {
        //Hardcoding connection strings is not a good idea. This will be better suited in a settings / config file
        //As this is a class library, we'll skip the idea of moving it into settings, rather, the better idea will be to have the consuming app provide any connection strings necessary
        private static string ConnectionString = @"Data Source=(local);Database=Orders;User Id=admin;Password=password;";

        /// <summary>
        /// Save / add the given order to the database
        /// </summary>
        /// <param name="order">Order to add to the database</param>
        public void Save(OrderModel order)
        {
            using (SqlConnection connection = GetSqlConnection())
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

        /// <summary>
        /// Gets all the orders from the provided database
        /// </summary>
        /// <returns>List of order models of orders in the database</returns>
        public List<OrderModel> GetAllOrders()
        {
            try
            {
                List<OrderModel> returnList = new List<OrderModel>();
                using (SqlConnection connection = GetSqlConnection())
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("SELECT * FROM Orders",
                        connection);                    

                    var reader = command.ExecuteReader();                    

                    while (reader.Read())
                    {
                        var order = new OrderModel();

                        order.Amount = double.Parse(reader["ammount"].ToString());
                        order.CustomerId = int.Parse(reader["customerId"].ToString());
                        order.OrderId = int.Parse(reader["orderId"].ToString());
                        order.VAT = double.Parse(reader["VAT"].ToString());

                        returnList.Add(order);
                    }                  

                    connection.Close();
                }

                return returnList;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Order repository error during get all: {e.Message}");
                return null;
            }            
        }

        #region Helpers


        /// <summary>
        /// Helper method to get a relevant connection string. Decouples/seperates concern from the methods using the connection.
        /// </summary>
        /// <returns></returns>
        private SqlConnection GetSqlConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        #endregion

    }
}

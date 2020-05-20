using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace AnyCompany
{
    public class OrderRepository : IOrderRepository
    {
        private static string ConnectionString;

        public OrderRepository(string connectionString)
        {
            ConnectionString = connectionString; 
        }

        // Changed name from Save to Add
        // Because the word "Save" infers it may be an idempotent opertaion like an "upsert"
        // the word "Add" is clearly tells what the method does
        // Could have used "Insert" as well
        public void Add(Order order)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = new SqlCommand("INSERT INTO Orders ([OrderId],[CustomerId],[Amount],[VAT]) VALUES (@OrderId, @CustomerId, @Amount, @VAT)", connection))
            {
                command.Parameters.AddWithValue("@OrderId", order.OrderId);
                command.Parameters.AddWithValue("@CustomerId", order.CustomerId);
                command.Parameters.AddWithValue("@Amount", order.Amount);
                command.Parameters.AddWithValue("@VAT", order.VAT);

                connection.Open();

                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public Dictionary<int, List<Order>> LoadOrdersForCustomers(IEnumerable<int> customerIds)
        {
            Dictionary<int, List<Order>> orders = new Dictionary<int, List<Order>>();
            string customerIdsParam = string.Join("','", customerIds);

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = new SqlCommand($"SELECT * FROM Orders WHERE CustomerId IN ('{customerIdsParam}')", connection))
            {
                connection.Open();
           
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var order = ExtractOrder(reader);
                        if (orders.ContainsKey(order.CustomerId))
                        {
                            orders[order.CustomerId].Add(order);
                        }
                        else
                        {
                            orders.Add(order.CustomerId, new List<Order>()
                            {
                                order
                            });
                        }
                    }
                }
                connection.Close();
            }

            return orders;
        }

        private Order ExtractOrder(SqlDataReader reader)
        {
            Order order = new Order
            {
                OrderId = (int)reader["OrderId"],
                CustomerId = (int)reader["CustomerId"],
                Amount = (double)reader["Amount"],
                VAT = (double)reader["VAT"]
            };

            return order;
        }

    }
}

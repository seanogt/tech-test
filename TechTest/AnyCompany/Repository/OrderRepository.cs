using System;
using System.Data.SqlClient;
using AnyCompany.Interface;

namespace AnyCompany
{
    internal class OrderRepository : IOrderRepository
    {
        public void Save(IOrder order)
        {
            const string query = "INSERT INTO Orders VALUES (@OrderId, @Amount, @VAT)";

            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.connOrders))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@OrderId", order.OrderId);
                        cmd.Parameters.AddWithValue("@Amount", order.Amount);
                        cmd.Parameters.AddWithValue("@VAT", order.VAT);
                        cmd.Parameters.AddWithValue("@CustomerId", order.CustomerId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}

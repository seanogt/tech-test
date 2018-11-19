using System.Data.SqlClient;
//using System.Reflection;

namespace AnyCompany
{
    internal class OrderRepository
    {
        private static string ConnectionString = @"Data Source=(local);Database=Orders;User Id=admin;Password=password;";

        public void Save(Order order, int customerId)
        {
            if (IsValidOrder(order) || customerId < 0) throw new System.Exception();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("INSERT INTO Orders VALUES (@OrderId, @Amount, @VAT, @CustomerId)", connection);

                command.Parameters.AddWithValue("@OrderId", order.OrderId);
                command.Parameters.AddWithValue("@Amount", order.Amount);
                command.Parameters.AddWithValue("@VAT", order.VAT);
                command.Parameters.AddWithValue("@CustomerId", customerId);

                command.ExecuteNonQuery();

            } // no try catch needed, 'using' gaurantees object to be disposed in case of exception
        }

        private bool IsValidOrder(Order order)
        {
            if (order == null) return false;
            if (order.Amount < 0 || order.OrderId < 0 || order.VAT < 0) return false;
            return true;
            
            // In order to write dynamic code, I can make use of Reflection. Assembly.GetExecutingAssembly and traverse through PropertyType and tryParsing it.
            // not needed in our simple test program
        }
    }
}
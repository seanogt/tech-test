using System.Configuration;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Linq;
using AnyCompany.Models;
using Dapper;

namespace AnyCompany.IntegrationTests.DataHelpers
{
    public static class OrderDataHelper
    {
        public static Order GetOrder(int id)
        {
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                connection.Open();
                return connection.Query<Order>(
                    "SELECT * FROM dbo.Orders WHERE OrderId = @OrderId", 
                    new { OrderId = id }).First();
            }
        }

        public static Order AddOrder(Order order)
        {
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                connection.Open();
                connection.Query<Order>(@"INSERT INTO Orders VALUES (@OrderId, @Amount, @VAT, @CustomerId)",
                    new
                    {
                        OrderId = order.OrderId,
                        Amount = order.Amount,
                        VAT = order.VAT,
                        CustomerId = order.CustomerId
                    });

                return order;
            }
        }

        private static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["OrderConnectionString"].ConnectionString;
        }
    }
}

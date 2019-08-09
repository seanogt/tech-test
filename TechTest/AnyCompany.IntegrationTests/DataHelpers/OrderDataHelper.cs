using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
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

        private static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["OrderConnectionString"].ConnectionString;
        }
    }
}

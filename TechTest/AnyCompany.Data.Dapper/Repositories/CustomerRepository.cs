using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using AnyCompany.Models;
using Dapper;

namespace AnyCompany.Data.Dapper.Repositories
{
    public static class CustomerRepository
    {
        public static Customer Load(int customerId)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["CustomerConnectionString"].ConnectionString;
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.Query<Customer>("SELECT * FROM Customer WHERE CustomerId = @CustomerId",
                    new {CustomerId = customerId}).FirstOrDefault();
            }
        }
    }
}

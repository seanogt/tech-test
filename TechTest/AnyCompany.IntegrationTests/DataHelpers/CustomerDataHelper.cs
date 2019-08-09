using System.Configuration;
using System.Data.SqlClient;
using DapperExtensions;

namespace AnyCompany.IntegrationTests.DataHelpers
{
    public static class CustomerDataHelper
    {
        public static Customer Add(Customer customer)
        {
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                connection.Open();
                int id = connection.Insert(customer);

                customer.CustomerId = id;
                return customer;
            }
        }

        private static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["CustomerConnectionString"].ConnectionString;
        }
    }
}
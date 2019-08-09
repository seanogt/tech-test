using System.Data;
using System.Data.SqlClient;
using AnyCompany.Data.Dapper.Enums;

namespace AnyCompany.Data.Dapper.Factories
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly string _customerConnectionString;
        private readonly string _orderDbConnectionString;

        public ConnectionFactory(string customerConnectionString, string orderDbConnectionString)
        {
            _customerConnectionString = customerConnectionString;
            _orderDbConnectionString = orderDbConnectionString;
        }

        public IDbConnection Create(ConnectionType connectionType)
        {
            var connectionString = connectionType == ConnectionType.CustomerDb
                ? _customerConnectionString
                : _orderDbConnectionString;

            return new SqlConnection(connectionString);
        }
    }
}

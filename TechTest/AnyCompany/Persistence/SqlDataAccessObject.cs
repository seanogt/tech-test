using System.Data;
using System.Data.SqlClient;

namespace AnyCompany.Persistence
{
    class SqlDataAccessObject : IDataAccessObject
    {
        private string _connectionString;

        public SqlDataAccessObject(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void CloseConnection(IDbConnection connection)
        {
            var sqlConnection = (SqlConnection)connection;

            sqlConnection.Close();
            sqlConnection.Dispose();
        }

        public IDbCommand CreateCommand(string commandText, CommandType commandType, IDbConnection connection)
        {
            SqlCommand sqlCommand = new SqlCommand
            {
                CommandText = commandText,
                Connection = (SqlConnection)connection,
                CommandType = commandType
            };

            return sqlCommand;
        }

        public IDbConnection CreateConnection()
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionString);

            return sqlConnection;
        }
    }
}

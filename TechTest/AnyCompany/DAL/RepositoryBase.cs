using System.Data;
using System.Data.SqlClient;

namespace AnyCompany.DAL
{
    public class RepositoryBase
    {
        private static string ConnectionString { get; set; }
        //Utilize connection pooling
        private static SqlConnection _connection;

        public static SqlConnection Connection
        {
            get
            {
                if (_connection == null || _connection.State != ConnectionState.Open)
                {
                    _connection = new SqlConnection(ConnectionString);
                    _connection.Open();
                }

                return _connection;
            }
        }

        public RepositoryBase(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public bool ExecuteCommand(SqlCommand command)
        {
            command.Connection = Connection;
            var saved = command.ExecuteNonQuery() > 0 ? true : false;

            return saved;
        }

        public SqlDataReader ExecuteQuery(SqlCommand command)
        {
            command.Connection = Connection;
            return command.ExecuteReader();
        }
    }
}

using System.Data;
using System.Data.SqlClient;

namespace AnyCompany.Persistence
{
    class DataParameterHelper
    {
        public static IDbDataParameter CreateParameter(string providerName, string name, object value, DbType dbType, ParameterDirection direction = ParameterDirection.Input)
        {
            switch (providerName.ToLower())
            {
                case "system.data.sqlclient":
                default:
                    return CreateSqlParameter(name, value, dbType, direction);
            }
        }

        public static IDbDataParameter CreateParameter(string providerName, string name, int size, object value, DbType dbType, ParameterDirection direction = ParameterDirection.Input)
        {
            switch (providerName.ToLower())
            {
                case "system.data.sqlclient":
                default:
                    return CreateSqlParameter(name, size, value, dbType, direction);
            }
        }

        private static IDbDataParameter CreateSqlParameter(string name, object value, DbType dbType, ParameterDirection direction)
        {
            SqlParameter sqlParameter = new SqlParameter
            {
                DbType = dbType,
                ParameterName = name,
                Direction = direction,
                Value = value
            };

            return sqlParameter;
        }

        private static IDbDataParameter CreateSqlParameter(string name, int size, object value, DbType dbType, ParameterDirection direction)
        {
            SqlParameter sqlParameter = new SqlParameter
            {
                DbType = dbType,
                Size = size,
                ParameterName = name,
                Direction = direction,
                Value = value
            };

            return sqlParameter;
        }
    }
}

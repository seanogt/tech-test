using System;
using System.Configuration;

namespace AnyCompany.Persistence
{
    class DataAccessObjectFactory
    {
        private string _connectionString;
        private ConnectionStringSettings _connectionStringSettings;

        public DataAccessObjectFactory(ConnectionStringSettings connectionStringSettings)
        {
            if (connectionStringSettings == null)
                throw new ArgumentNullException();

            _connectionStringSettings = connectionStringSettings;
        }

        public DataAccessObjectFactory(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException();

            _connectionString = connectionString;
        }

        public IDataAccessObject CreateDataAccess()
        {
            IDataAccessObject databaseObject;

            if (_connectionStringSettings != null)
            {
                switch (_connectionStringSettings.ProviderName.ToLower())
                {
                    case "system.data.sqlclient":
                    default:
                        databaseObject = new SqlDataAccessObject(_connectionStringSettings.ConnectionString);
                        break;
                }
            }
            else
            {
                //Default to Sql Client connection. More work can be done to attempt to find the correct provider and create the correct object
                databaseObject = new SqlDataAccessObject(_connectionString);
            }

            return databaseObject;
        }
    }
}

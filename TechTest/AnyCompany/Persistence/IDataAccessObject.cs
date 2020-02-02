using System.Data;

namespace AnyCompany.Persistence
{
    interface IDataAccessObject
    {
        IDbConnection CreateConnection();

        void CloseConnection(IDbConnection connection);

        IDbCommand CreateCommand(string commandText, CommandType commandType, IDbConnection connection);
    }
}

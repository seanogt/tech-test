using System.Data;
using AnyCompany.Data.Dapper.Enums;

namespace AnyCompany.Data.Dapper.Factories
{
    public interface IConnectionFactory
    {
        IDbConnection Create(ConnectionType connectionType);
    }
}

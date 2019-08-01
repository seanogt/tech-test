using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnyCompany.Service.DAL
{
    public interface IDatabaseWrapper
    {
        Task<IEnumerable<IDictionary<string, object>>> ExecuteSqlFile(string fileName, params object[] args);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnyCompany.Service.DAL
{
    public interface IDatabaseWrapper
    {
        /// <summary>
        /// Executes an SQL file, located the Resources folder of the project
        /// </summary>
        /// <param name="fileName">Name of SQL file to execute</param>
        /// <param name="args">Arguments to pass to the query. Will be passed by index.</param>
        /// <returns>A list of rows. Each row has a Dictionary which maps each column to the respective value of the row in this column.</returns>
        Task<IEnumerable<IDictionary<string, object>>> ExecuteSqlFile(string fileName, IEnumerable<object> args);
        
        // TODO - query optimisation: An implementation of arguments by key-value pairs, instead of index based.
        // TODO - boxing of arguments; Currently all arguments will be boxed to objects. This can be avoided by better generics implementation.
    }
}
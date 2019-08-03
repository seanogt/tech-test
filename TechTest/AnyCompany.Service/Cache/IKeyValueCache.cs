using System.Threading.Tasks;

namespace AnyCompany.Service.Cache
{
    public interface IKeyValueCache
    {
        /// <summary>
        /// Fetches value by key, returns null if nothing was found
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<string> Get(string key);
        
        /// <summary>
        /// Sets the value for the specified key, overwriting existing values.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task Set(string key, string value);
    }
}
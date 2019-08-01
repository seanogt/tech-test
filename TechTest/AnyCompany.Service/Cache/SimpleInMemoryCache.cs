using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnyCompany.Service.Cache
{
    public class SimpleInMemoryCache : IKeyValueCache
    {
        private readonly Dictionary<string, string> _cache;

        public SimpleInMemoryCache()
        {
            _cache  = new Dictionary<string, string>();    
        }
        
        public async Task<string> Get(string key)
        {
            return await Task.Run(() => _cache[key]);
        }

        public async Task Set(string key, string value)
        {
            await Task.Run(() => _cache[key] = value);
        }
    }
}
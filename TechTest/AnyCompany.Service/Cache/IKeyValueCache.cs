using System.Threading.Tasks;

namespace AnyCompany.Service.Cache
{
    public interface IKeyValueCache
    {
        Task<string> Get(string key);
        Task Set(string key, string value);
    }
}
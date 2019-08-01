using System.Linq;
using System.Threading.Tasks;

namespace AnyCompany.Data
{
    public interface IRepository<T> where T : class
    {
        T Add(T t);
        Task<T> AddAsync(T t);

        T Load(int id);
        Task<T> LoadAsync(int id);

        IQueryable<T> LoadAll();
    }
}

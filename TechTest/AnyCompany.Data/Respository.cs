using System.Linq;
using System.Threading.Tasks;

namespace AnyCompany.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public Repository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public T Add(T t)
        {
            _applicationDbContext.Set<T>().Add(t);
            _applicationDbContext.SaveChanges();
            return t;
        }

        public async Task<T> AddAsync(T t)
        {
            _applicationDbContext.Set<T>().Add(t);
            await _applicationDbContext.SaveChangesAsync();
            return t;
        }

        public T Load(int id)
        {
            return _applicationDbContext.Set<T>().Find(id);
        }

        public async Task<T> LoadAsync(int id)
        {
            return await _applicationDbContext.Set<T>().FindAsync(id);
        }

        public IQueryable<T> LoadAll()
        {
            return _applicationDbContext.Set<T>();
        }
    }
}

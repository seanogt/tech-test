using System.Threading.Tasks;
using AnyCompany.Models;

namespace AnyCompany.Data.Contract.Repositories
{
    public interface IOrderRepository
    {
        void Add(Order order);
    }
}

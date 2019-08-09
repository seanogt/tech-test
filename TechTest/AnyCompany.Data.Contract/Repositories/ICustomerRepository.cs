using AnyCompany.Models;

namespace AnyCompany.Data.Contract.Repositories
{
    public interface ICustomerRepository
    {
        Customer Load(int customerId);
    }
}

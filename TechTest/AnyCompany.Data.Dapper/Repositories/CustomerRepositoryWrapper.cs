using AnyCompany.Data.Contract.Repositories;
using AnyCompany.Models;

namespace AnyCompany.Data.Dapper.Repositories
{
    public class CustomerRepositoryWrapper : ICustomerRepository
    {
        // using this to act as a proxy to the static CustomerRepository - I assumed this was legacy that 
        // we're trying to abstract and eventually replace
        public Customer Load(int customerId)
        {
            return CustomerRepository.Load(customerId);
        }
    }
}

using AnyCompany.Entity;
using System.Collections.Generic;
using System.Linq;

namespace AnyCompany.Repository
{
    public static class CustomerRepository
    {
        public static Customer Load(int customerId, IUnitOfWork unitOfWork)
        {
            return unitOfWork.Customers.SingleOrDefault<Customer>(c => c.CustomerId == customerId);
        }

        public static IEnumerable<Customer> LoadAll(IUnitOfWork unitOfWork)
        {
            return unitOfWork.Customers.ToList<Customer>();
        }
    }
}

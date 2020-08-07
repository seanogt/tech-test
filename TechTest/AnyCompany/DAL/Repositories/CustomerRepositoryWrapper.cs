using System.Collections.Generic;

namespace AnyCompany.DAL.Repositories
{
    public class CustomerRepositoryWrapper : ICustomerRepository
    {
        public Customer Get(int CustomerId)
        {
            //Use a mapper like AutoMapper here to change Model to user friendly DTO if necessary
            return CustomerRepository.Get(CustomerId);
        }

        public IEnumerable<Customer> GetAll(bool includeOrders = true)
        {
            //Use a mapper like AutoMapper here to change Model to user friendly DTO if necessary
            return CustomerRepository.GetAll(includeOrders);
        }
    }
}

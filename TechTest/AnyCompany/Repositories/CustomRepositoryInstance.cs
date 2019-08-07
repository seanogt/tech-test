using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.Repositories
{
    internal class CustomRepositoryInstance : ICustomerRepository
    {
        public Customer Load(int customerId)
        {
            return CustomerRepository.Load(customerId);
        }
    }
}

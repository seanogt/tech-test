using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public interface ICustomerRepository
    {
        Customer Load(int customerId);
    }
}

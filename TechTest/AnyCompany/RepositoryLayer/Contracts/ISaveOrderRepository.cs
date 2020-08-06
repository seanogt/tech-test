using AnyCompany.Entities;
using AnyCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.RepositoryLayer.Contracts
{
    public interface ISaveOrderRepository<T> where T : class
    {
        bool SaveOrder(NewOrder order);
    }
}

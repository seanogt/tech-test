using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.Interface
{
    public interface IOrderRepository
    {
        void Save(IOrder order);
    }
}

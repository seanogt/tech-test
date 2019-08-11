using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public interface IOrderRepository
    {
        void Save(Order order);
    }
}

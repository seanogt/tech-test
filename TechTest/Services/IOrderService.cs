using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public interface IOrderService
    {
        bool PlaceOrder(Order order, int customerId);
    }
}

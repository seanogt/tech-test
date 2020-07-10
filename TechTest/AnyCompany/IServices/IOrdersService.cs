using System;
using System.Collections.Generic;

namespace AnyCompany.IServices
{
    public interface IOrdersService 
    {
        bool PlaceOrder(Order order, int customerId);
        Order GetOrder(int orderId);
    }
}

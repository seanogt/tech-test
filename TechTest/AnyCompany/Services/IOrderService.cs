using AnyCompany.Dtos;

namespace AnyCompany.Services
{
    interface IOrderService
    {
        bool PlaceOrder(OrderDto order, int customerId);
    }
}

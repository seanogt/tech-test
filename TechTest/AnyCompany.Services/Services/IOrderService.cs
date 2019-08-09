using AnyCompany.Services.Dtos;

namespace AnyCompany.Services.Services
{
    interface IOrderService
    {
        bool PlaceOrder(OrderDto order, int customerId);
    }
}

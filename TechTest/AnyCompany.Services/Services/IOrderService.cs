using AnyCompany.Services.Dtos;

namespace AnyCompany.Services.Services
{
    public interface IOrderService
    {
        bool PlaceOrder(OrderDto order, int customerId);
    }
}

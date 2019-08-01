using AnyCompany.Service.Requests;

namespace AnyCompany.Service
{
    public interface IOrderService
    {
        bool PlaceOrder(PlaceOrderRequest request);
    }
}

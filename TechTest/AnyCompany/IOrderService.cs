namespace AnyCompany.Utilities
{
    public interface IOrderService
    {
        bool PlaceOrder(Order order, int customerId);
    }
}

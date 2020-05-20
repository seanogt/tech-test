namespace AnyCompany
{
    public interface IOrderService
    {
        bool PlaceOrder(Order order, int customerId);
    }
}
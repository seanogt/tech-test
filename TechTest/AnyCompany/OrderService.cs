namespace AnyCompany
{
    public class OrderService
    {
        private readonly OrderRepository orderRepository = new OrderRepository();

        public bool PlaceOrder(Order order, int customerId)
        {
            if (order == null || customerId < 0) return false;

            Customer customer = CustomerRepository.Load(customerId);

            if (customer == null) return false;

            if (order.Amount == 0)
                return false;

            if (customer.Country == "UK")
                order.VAT = 0.2f;
            else
                order.VAT = 0;

            try
            {
                orderRepository.Save(order, customerId);
            }
            catch
            {
                return false;
            }
                
            return true;
        }
    }
}

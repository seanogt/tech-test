namespace AnyCompany
{
    public class OrderService
    {
        private readonly OrderRepository orderRepository1=new OrderRepository();

        public bool PlaceOrder(Order order, int customerId)
        {
            Customer customer = CustomerRepository.Load(customerId);

            if (order.Amount == 0)
                return false;

            if (customer.Country == "UK")
                order.VAT = 0.2d;
            else
                order.VAT = 0;

            orderRepository1.Save(order);

            return true;
        }
    }
}

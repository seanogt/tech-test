namespace AnyCompany
{
    public class OrderService
    {
        private readonly OrderRepository orderRepository = new OrderRepository();

        public bool PlaceOrder(Order order, int customerId)
        {
            Customer customer = LoadACustomerFromCustomerRepository(customerId);

            if (order.Amount == 0)
                return false;

            order = SetVAT(order, customer);

            SaveOrderToOrderRepository(order);

            return true;
        }

        public Order SetVAT(Order order, Customer customer)
        {
            if (customer.Country == "UK")
                order.VAT = 0.2d;
            else
                order.VAT = 0;

            return order;
        }

        protected virtual void SaveOrderToOrderRepository(Order order)
        {
            orderRepository.Save(order);
        }

        protected virtual Customer LoadACustomerFromCustomerRepository(int customerId)
        {
            Customer customer = CustomerRepository.Load(customerId);
            return customer;
        }
    }
}

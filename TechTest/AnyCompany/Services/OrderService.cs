namespace AnyCompany
{
    public class OrderService
    {
        private readonly OrderRepository orderRepository = new OrderRepository();

        public bool PlaceOrder(Order order, int customerId)
        {
            Customer customer = LoadACustomerFromCustomerRepository(customerId);

            if (!order.IsValid())
                return false;

           
            SaveOrderToOrderRepository(order.SetVAT(customer));

            return true;
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

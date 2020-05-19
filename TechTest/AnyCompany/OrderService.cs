using AnyCompany.Interface;

namespace AnyCompany
{
    public class OrderService : IOrderService
    {
        private readonly OrderRepository orderRepository = new OrderRepository();

        public bool PlaceOrder(Order order, int customerId, out string message)
        {
            message = "";

            if(customerId == 0)
            {
                message = "Error, Customer ID should be greater than 0";
                return false;
            }

            Customer customer = CustomerRepository.Load(customerId);

            order.CustomerID = customer.CustomerID;

            if (order.Amount <= 0) {
                message = "Amount cannot be zero or less.";
                return false;
            }

            if (customer.Country == "UK")
                order.VAT = 0.2d;
            else
                order.VAT = 0;

            orderRepository.Save(order);

            return true;
        }
    }
}

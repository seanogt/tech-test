using AnyCompany.Models;
using AnyCompany.Repositories.OrderRepository;

namespace AnyCompany
{
    public class OrderService
    {
        private readonly OrderRepository orderRepository = new OrderRepository();

        public bool PlaceOrder(Order order, int customerId)
        {
            Customer customer = CustomerRepository.Load(customerId);

            if (order.Amount == 0)
                return false;

            if (customer.Address.Country == "UK")
                order.VAT = 0.2d;
            else
                order.VAT = 0;

            orderRepository.Save(order);

            return true;
        }
    }
}

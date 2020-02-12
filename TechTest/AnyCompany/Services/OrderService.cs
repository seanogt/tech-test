using AnyCompany.Models;
using AnyCompany.Repositories;

namespace AnyCompany.Services
{
    public class OrderService
    {
        private readonly OrderRepository orderRepository = new OrderRepository();

        public bool PlaceOrder(Order order, int customerId)
        {
            Customer customer = CustomerRepository.Load(customerId);

            if (order.Amount == 0)
                return false;

            if (customer.Country == "UK")
                order.Vat = 0.2d;
            else
                order.Vat = 0;

            orderRepository.Save(order);

            return true;
        }
    }
}

using AnyCompany.Interfaces;
using AnyCompany.Models;
using AnyCompany.Repositories;

namespace AnyCompany
{
    public class OrderService : IOrderService
    {
        private readonly OrderRepository orderRepository = new OrderRepository();

        public bool PlaceOrder(Order order, int customerId, out string error)
        {
            error = "";
            if (customerId <= 0)
            {
                error = "Please provide a correct Customer Id and try again";
                return false;
            }
            Customer customer = CustomerRepository.Load(customerId);

            order.CustomerId = customer.CustomerId;
            if (order.Amount <= 0) {
                error = "Amount can not be zero or below, please provide a correct amount and try again";
                return false;
            }

            if (customer.Country == "UK")
            {
                order.VAT = 0.2d;
            }
            else
            {
                order.VAT = 0;
            }

            orderRepository.Save(order);

            return true;
        }
    }
}

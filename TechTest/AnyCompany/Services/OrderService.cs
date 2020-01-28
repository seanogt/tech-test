using AnyCompany.Helpers;
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

            order.VAT = VatHelper.GetVatRateByCountry(customer.Country);

            orderRepository.Save(order);

            return true;
        }
    }
}

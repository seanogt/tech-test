using System;
using AnyCompany.Interface;

namespace AnyCompany
{
    public class OrderService
    {
        private readonly OrderRepository orderRepository = new OrderRepository();

        public bool PlaceOrder(IOrder order, Guid customerId)
        {
            var customer = CustomerRepository.Load(customerId);

            if (order.Amount < 0.0)
                return false;

            order.VAT = customer.Country == "UK" ? Properties.Settings.Default.ukVat : Properties.Settings.Default.otherVat;

            order.CustomerId = customerId;
            orderRepository.Save(order);

            return true;
        }
    }
}

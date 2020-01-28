using AnyCompany.Helpers;
using AnyCompany.Models;
using AnyCompany.Repositories;
using System;

namespace AnyCompany.Services
{
    /// <summary>
    /// This is the Order Service, and main entry point for this library. Orders are managed from here.
    /// </summary>
    public class OrderService
    {
        private readonly OrderRepository orderRepository = new OrderRepository();

        /// <summary>
        /// This places and persists an order.
        /// </summary>
        /// <param name="order">The order information.</param>
        /// <param name="customerId">The Id of the customer this order is placed agains.</param>
        public bool PlaceOrder(Order order, int customerId)
        {
            Customer customer = CustomerRepository.Load(customerId);

            if (customer == null)
                throw new ArgumentException("Customer could not be loaded. Invalid customer Id.");

            if (order.Amount == 0)
                return false;

            order.VAT = VatHelper.GetVatRateByCountry(customer.Country);
            order.CustomerId = customer.CustomerId;
            orderRepository.Save(order);

            return true;
        }
    }
}

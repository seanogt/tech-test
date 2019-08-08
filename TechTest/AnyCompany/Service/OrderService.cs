// <copyright file="OrderService.cs" company="Investec Bank">
// Copyright © Investec Bank 2018
// </copyright>

namespace AnyCompany.Service
{
    using AnyCompany.Repository;

    /// <summary>
    /// Contains business level logic for interacting with orders, specifically providing order placing functionality.
    /// </summary>
    public class OrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly ICustomerRepository customerRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderService"/> class.
        /// </summary>
        /// <param name="orderRepository">The order repository.</param>
        /// <param name="customerRepository">The customer repository.</param>
        public OrderService(IOrderRepository orderRepository, ICustomerRepository customerRepository)
        {
            this.orderRepository = orderRepository;
            this.customerRepository = customerRepository;
        }

        /// <summary>
        /// Attaches a given order to the customer with the given customerId.
        /// </summary>
        /// <param name="order">The order to be placed.</param>
        /// <returns>True if the order is placed successfully, false otherwise.</returns>
        public bool PlaceOrder(Order order)
        {
            Customer customer = this.customerRepository.Load(order.CustomerId);

            if (order.Amount == 0)
            {
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

            this.orderRepository.Save(order);

            return true;
        }
    }
}

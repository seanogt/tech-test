// <copyright file="CustomerService.cs" company="Investec Bank">
// Copyright © Investec Bank 2018
// </copyright>

namespace AnyCompany.Service
{
    using AnyCompany.Repository;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Contains business level logic for interacting with customers, specifically customer loading functionality.
    /// </summary>
    public class CustomerService
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IOrderRepository orderRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerService"/> class.
        /// </summary>
        /// <param name="customerRepository">The customer repository.</param>
        /// <param name="orderRepository">The order repository.</param>
        public CustomerService(ICustomerRepository customerRepository, IOrderRepository orderRepository)
        {
            this.customerRepository = customerRepository;
            this.orderRepository = orderRepository;
        }

        /// <summary>
        /// Loads all customers, along with their orders.
        /// </summary>
        /// <returns>A set of (customer, set of orders) tuples.</returns>
        public IEnumerable<(Customer, IEnumerable<Order>)> LoadAllCustomersWithOrders()
        {
            var allCustomers = this.customerRepository.LoadAll();
            var allOrders = this.orderRepository.LoadAll();

            var customerIdToOrdersMapping = allCustomers.ToDictionary(c => c.CustomerId, c => new List<Order>());
            foreach (var order in allOrders)
            {
                customerIdToOrdersMapping[order.CustomerId].Add(order);
            }

            return allCustomers.Select(c => (c, customerIdToOrdersMapping[c.CustomerId].AsEnumerable()));
        }
    }
}

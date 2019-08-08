// <copyright file="MockOrderRepository.cs" company="Investec Bank">
// Copyright © Investec Bank 2018
// </copyright>

namespace AnyCompany.Tests.MockRepository
{
    using AnyCompany.Repository;
    using System.Collections.Generic;

    /// <summary>
    /// Provides a mock implementation of IOrderRepository for use during unit testing, that starts out empty.
    /// </summary>
    internal class MockOrderRepository : IOrderRepository
    {
        private readonly List<Order> orders = new List<Order>();

        /// <inheritdoc/>
        public void Save(Order order)
        {
            this.orders.Add(order);
        }

        /// <inheritdoc/>
        public IEnumerable<Order> LoadAll()
        {
            return this.orders;
        }
    }
}

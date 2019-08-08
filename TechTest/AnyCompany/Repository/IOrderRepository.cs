// <copyright file="IOrderRepository.cs" company="Investec Bank">
// Copyright © Investec Bank 2018
// </copyright>

namespace AnyCompany.Repository
{
    using System.Collections.Generic;

    /// <summary>
    /// Provides methods for saving and retrieving orders.
    /// </summary>
    public interface IOrderRepository
    {
        /// <summary>
        /// Saves a given order to the database.
        /// </summary>
        /// <param name="order">The order to save.</param>
        void Save(Order order);

        /// <summary>
        /// Loads all orders.
        /// </summary>
        /// <returns>The set of all orders.</returns>
        IEnumerable<Order> LoadAll();
    }
}

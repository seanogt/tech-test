// <copyright file="IOrderRepository.cs" company="Investec Bank">
// Copyright © Investec Bank 2018
// </copyright>

namespace AnyCompany.Repository
{
    /// <summary>
    /// Provides methods for saving (and in future retrieving) orders.
    /// </summary>
    public interface IOrderRepository
    {
        /// <summary>
        /// Saves a given order to the database.
        /// </summary>
        /// <param name="order">The order to save.</param>
        void Save(Order order);
    }
}

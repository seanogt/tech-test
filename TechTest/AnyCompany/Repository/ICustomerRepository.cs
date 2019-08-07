// <copyright file="ICustomerRepository.cs" company="Investec Bank">
// Copyright © Investec Bank 2018
// </copyright>

namespace AnyCompany.Repository
{
    /// <summary>
    /// Provdies methods for loading (and in future saving) <see cref="Customer"/> objects.
    /// </summary>
    public interface ICustomerRepository
    {
        /// <summary>
        /// Loads a cutomer by it's customerId.
        /// </summary>
        /// <param name="customerId">The id of the customer to load.</param>
        /// <returns>The customer with the given id, if one exists.</returns>
        Customer Load(int customerId);
    }
}

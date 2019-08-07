// <copyright file="CustomerRepositoryInstance.cs" company="Investec Bank">
// Copyright © Investec Bank 2018
// </copyright>

namespace AnyCompany.Repository
{
    /// <summary>
    /// An instantiable wrapper for the <see cref="CustomerRepository"/>, providing a SqlClient implementation of
    /// <see cref="ICustomerRepository"/>.
    /// </summary>
    public class CustomerRepositoryInstance : ICustomerRepository
    {
        /// <summary>
        /// Loads a cutomer by it's customerId.
        /// </summary>
        /// <param name="customerId">The id of the customer to load.</param>
        /// <returns>The customer with the given id, if one exists.</returns>
        public Customer Load(int customerId)
        {
            return CustomerRepository.Load(customerId);
        }
    }
}

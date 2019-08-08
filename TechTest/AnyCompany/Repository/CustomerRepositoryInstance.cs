// <copyright file="CustomerRepositoryInstance.cs" company="Investec Bank">
// Copyright © Investec Bank 2018
// </copyright>

namespace AnyCompany.Repository
{
    using System.Collections.Generic;

    /// <summary>
    /// An instantiable wrapper for the <see cref="CustomerRepository"/>, providing a SqlClient implementation of
    /// <see cref="ICustomerRepository"/>.
    /// </summary>
    public class CustomerRepositoryInstance : ICustomerRepository
    {
        /// <inheritdoc/>
        public Customer Load(int customerId)
        {
            return CustomerRepository.Load(customerId);
        }

        /// <inheritdoc/>
        public IEnumerable<Customer> LoadAll()
        {
            return CustomerRepository.LoadAll();
        }
    }
}

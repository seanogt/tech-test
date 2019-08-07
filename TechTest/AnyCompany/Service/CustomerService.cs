// <copyright file="CustomerService.cs" company="Investec Bank">
// Copyright © Investec Bank 2018
// </copyright>

namespace AnyCompany.Service
{
    using AnyCompany.Repository;

    /// <summary>
    /// Contains business level logic for interacting with customers, specifically customer loading functionality.
    /// </summary>
    public class CustomerService
    {
        private readonly ICustomerRepository customerRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerService"/> class.
        /// </summary>
        /// <param name="customerRepository">The customer repository.</param>
        public CustomerService(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }
    }
}
